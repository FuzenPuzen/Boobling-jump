using Zenject;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;
using DG.Tweening;
using EventBus;

public class ButtonsPanelView : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _takeBonusButton;
    [SerializeField] private Button _takeMoneyButton;
    [SerializeField] private TMP_Text _collectedCoinsText;
    [SerializeField] private HorizontalLayoutGroup _layoutGroup;
    [SerializeField] private TMP_Text _rewardBonusText;

    [SerializeField] private GameObject _adButtons;
    [SerializeField] private GameObject _navigationButtons;


    private float _changeTypeDelay = 0.25f;
    private IEnumerator _changeTypeDelayCor;
    private RewardBonusType _currentRewardBonusType;
    public Action OnSelectBonusType;
    public Action OnTakeMoney;

    private int _collectedCoinsCount;

    public void SetData(int collectedCoinsCount)
    {
        _collectedCoinsCount = collectedCoinsCount;
        _takeBonusButton.onClick.AddListener(SelectBonusType);
        _takeMoneyButton.onClick.AddListener(TakeMoney);

        _menuButton.onClick.AddListener(() => EventBus<OnOpenMenu>.Raise());
        _restartButton.onClick.AddListener(() => EventBus<OnRestart>.Raise());

        _takeMoneyButton.onClick.AddListener(TakeMoney);
        FillPanel();
        ActivateView();
    }

    private void SelectBonusType()
    {
        OnSelectBonusType?.Invoke();
        ShowNavigationButtons();
        _collectedCoinsText.text = (_collectedCoinsCount * (int)RewardBonusType.X5).ToString();
    }

    private void TakeMoney()
    {
        OnTakeMoney?.Invoke();
        ShowNavigationButtons();
    }

    private void ShowNavigationButtons()
    {
        _adButtons.SetActive(false);
        _navigationButtons.SetActive(true);
    }

    public void FillPanel()
    {
        _collectedCoinsText.text = _collectedCoinsCount.ToString();
        StartCoroutine(ShowDelay(3));
    }

    private IEnumerator ShowDelay(float time)
    {
        yield return new  WaitForSecondsRealtime(time);
        ShowTakeButton();
    }

    public void ShowTakeButton()
    {
        float time = 0.5f;
        DOTween.To(() => _layoutGroup.padding.left, x => _layoutGroup.padding.left = x, 50, time);
        DOTween.To(() => _layoutGroup.spacing, x => _layoutGroup.spacing = x, 40, time);
    }

    public void ActivateView()
    {
        ChangeType();
    }


    public RewardBonusType StopChangeType()
    {
        StopCoroutine(_changeTypeDelayCor);
        return RewardBonusType.X5;
    }

    private void ChangeType()
    {
        _currentRewardBonusType = RandomEnumElement<RewardBonusType>.GetRandomEnumElement();
        _rewardBonusText.text = _currentRewardBonusType.ToString();
        _changeTypeDelayCor = ChangeTypeDelayCor();
        StartCoroutine(_changeTypeDelayCor);
    }

    private IEnumerator ChangeTypeDelayCor()
    {
        yield return new WaitForSecondsRealtime(_changeTypeDelay);
        ChangeType();
    }

}

public class ButtonsPanelViewService : IService
{
	private IViewFabric _fabric;
	private ButtonsPanelView _ButtonsPanelView;
    private IMarkerService _markerService;
	private ICoinDataManager _coinDataManager;
    private RewardBonusType _rewardBonusType;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService, ICoinDataManager coinDataManager)
	{
		_markerService = markerService;
		_fabric = fabric;
		_coinDataManager = coinDataManager;
	}

	public void ActivateService()
	{
        Transform parent = _markerService.GetTransformMarker<EndPageMarker>().transform;
        _ButtonsPanelView = _fabric.Init<ButtonsPanelView>(parent);
        _ButtonsPanelView.OnSelectBonusType = OnSelectBonusType;
        _ButtonsPanelView.OnTakeMoney = OnTakeMoney;
        _ButtonsPanelView.SetData(_coinDataManager.GetSesionCollectedCoins());
    }

    private void OnSelectBonusType()
    {
        _rewardBonusType = _ButtonsPanelView.StopChangeType();
        //Добавить рекламу
        _coinDataManager.AddCoins(_coinDataManager.GetSesionCollectedCoins() * ((int)_rewardBonusType - 1));
    }

    private void OnTakeMoney()
    {
        _coinDataManager.AddCoins(_coinDataManager.GetSesionCollectedCoins());
    }

}
public enum RewardBonusType
{
    X2 = 2,
    X3 = 3,
    X5 = 5
}

public class RandomEnumElement<T> where T : Enum
{
    private static System.Random random = new System.Random();
    private static List<T> usedElements = new List<T>();

    public static T GetRandomEnumElement()
    {
        Array values = Enum.GetValues(typeof(T));

        List<T> unusedElements = new List<T>();
        foreach (T value in values)
        {
            if (!usedElements.Contains(value))
            {
                unusedElements.Add(value);
            }
        }

        if (unusedElements.Count == 0)
        {
            // Если все элементы использованы, очистите список и начните заново
            usedElements.Clear();
            unusedElements.AddRange((T[])values);
        }

        int randomIndex = random.Next(unusedElements.Count);
        T randomElement = unusedElements[randomIndex];
        usedElements.Add(randomElement);

        return randomElement;
    }
}
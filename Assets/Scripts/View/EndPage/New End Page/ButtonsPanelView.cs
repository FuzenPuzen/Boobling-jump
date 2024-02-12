using Zenject;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;

public class ButtonsPanelView : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _takeBonusButton;
    [SerializeField] private Button _takeMoneyButton;
    [SerializeField] private TMP_Text _collectedCoinsText;

    [SerializeField] private TMP_Text _rewardBonusText;
    private float _changeTypeDelay = 0.25f;
    private IEnumerator _changeTypeDelayCor;
    private RewardBonusType _currentRewardBonusType;

    private int _collectedCoinsCount;

    public void SetData(int collectedCoinsCount)
    {
        _collectedCoinsCount = collectedCoinsCount;
        FillPanel();
        ActivateView();
    }

    public void FillPanel()
    {
        _collectedCoinsText.text = _collectedCoinsCount.ToString();
    }

    public void ActivateView()
    {
        ChangeType();
    }

    public void ShowView()
    {

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
        yield return new WaitForSeconds(_changeTypeDelay);
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
        _ButtonsPanelView.SetData(_coinDataManager.GetSesionCollectedCoins());
    }

    private void OnSelectBonusType()
    {
        _rewardBonusType = _ButtonsPanelView.StopChangeType();
        //Добавить рекламу
        _coinDataManager.AddCoins(_coinDataManager.GetSesionCollectedCoins() * ((int)_rewardBonusType - 1));
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
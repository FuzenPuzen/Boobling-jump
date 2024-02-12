using Zenject;
using UnityEngine;
using UnityEngine.UI;
using EventBus;
using EventBus.Example.MyGame.Events;
using TMPro;
using DG.Tweening;

public class MenuTutorialPanelView : MonoBehaviour
{
    [SerializeField] internal Button _gameButton;
    [SerializeField] internal CanvasGroup _scoreObj;
    [SerializeField] internal Image _filledCircle;
    [SerializeField] internal TMP_Text _remaidScore;
    [SerializeField] internal TMP_Text _percentScoreText;
    [SerializeField] internal TMP_Text _rewardCount;
    internal ScoreRewardDataPackage _scoreRewardDataPackage;
    private int _recordScore;
    internal float _percentScore;
    private float _fadeTime = 1f;
    private Sequence _fadeSequence;

    private void Start()
    {
        _gameButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        _fadeSequence.Kill();
        EventBus<OnClickGame>.Raise();
    }

    public void SetData(ScoreRewardDataPackage scoreRewardDataPackage, int recordScore)
    {
        _scoreRewardDataPackage = scoreRewardDataPackage;
        _recordScore = recordScore;
        UpdateView();
    }

    public virtual void UpdateView()
    {
        float filledAmount = (float)_recordScore / (float)_scoreRewardDataPackage.RewardScore;
        _percentScore = filledAmount * 100;
        _percentScoreText.text = _percentScore + "%";
        _filledCircle.fillAmount = filledAmount;
        _remaidScore.text = _recordScore.ToString();
        StartFade();
    }

    public void StartFade()
    {
        _fadeSequence = DOTween.Sequence();
        _fadeSequence.SetLoops(-1);
        _fadeSequence.Append(_scoreObj.DOFade(0, _fadeTime));
        _fadeSequence.Append(_percentScoreText.DOFade(1, _fadeTime));
        _fadeSequence.AppendInterval(_fadeTime * 3);
        _fadeSequence.Append(_percentScoreText.DOFade(0, _fadeTime));
        _fadeSequence.Append(_scoreObj.DOFade(1, _fadeTime));
        _fadeSequence.AppendInterval(_fadeTime * 3);

    }
}

public class MenuTutorialPanelViewService : IService
{
    private IViewFabric _fabric;
    private MenuTutorialPanelView _menuTutorialShopPanelView;
    private IMarkerService _markerService;
    private IScoreDataManager _scoreDataManager;

    [Inject]
    public void Constructor(IViewFabric fabric, IMarkerService markerService, IScoreDataManager scoreDataManager)
    {
        _scoreDataManager = scoreDataManager;
        _markerService = markerService;
        _fabric = fabric;
    }

    public void ActivateService()
    {
        Transform parent = _markerService.GetTransformMarker<MenuMainPageMarker>().transform;
        _menuTutorialShopPanelView = _fabric.Init<MenuTutorialPanelView>(parent);
        _menuTutorialShopPanelView.SetData(_scoreDataManager.GetScoreRewardDataPackage(), _scoreDataManager.GetRecordScore());
    }
}

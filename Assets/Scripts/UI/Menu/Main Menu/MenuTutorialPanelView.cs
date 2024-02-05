using Zenject;
using UnityEngine;
using UnityEngine.UI;
using EventBus;
using EventBus.Example.MyGame.Events;
using TMPro;

public class MenuTutorialPanelView : MonoBehaviour
{
    [SerializeField] private Button _gameButton;
    [SerializeField] private Image _filledCircle;
    [SerializeField] private TMP_Text _remaidScore;
    [SerializeField] private TMP_Text _rewardScore;
    [SerializeField] private TMP_Text _rewardCount;
    private ScoreRewardDataPackage _scoreRewardDataPackage;

    private void Start()
    {
        _gameButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        EventBus<OnClickGame>.Raise();
    }

    public void SetData(ScoreRewardDataPackage scoreRewardDataPackage)
    {
        _scoreRewardDataPackage = scoreRewardDataPackage;
        UpdateView();
    }

    public void UpdateView()
    {
        float filledAmount = (float)_scoreRewardDataPackage.RemaindScore / (float)_scoreRewardDataPackage.RewardScore;
        _filledCircle.fillAmount = filledAmount;

        _rewardCount.text = _scoreRewardDataPackage.RewardCount.ToString();
        _rewardScore.text = "\n из" + _scoreRewardDataPackage.RewardScore.ToString();
        _remaidScore.text = _scoreRewardDataPackage.RemaindScore.ToString();
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
        _menuTutorialShopPanelView.SetData(_scoreDataManager.GetScoreRewardDataPackage());
    }
}

using Zenject;
using UnityEngine;
using EventBus;
using UnityEngine.UI;

public class MenuInfinityPanelView : MenuTutorialPanelView
{

    public override void UpdateView()
    {
        float filledAmount = (float)_scoreRewardDataPackage.RemaindScore / (float)_scoreRewardDataPackage.RewardScore;
        _filledCircle.fillAmount = filledAmount;
        _percentScore = filledAmount * 100;
        _percentScoreText.text = _percentScore + "%";
        _rewardCount.text = _scoreRewardDataPackage.RewardCount.ToString();
        _remaidScore.text = (_scoreRewardDataPackage.RewardScore - _scoreRewardDataPackage.RemaindScore).ToString();
        StartFade();
    }
}

public class MenuInfinityPanelViewService : IService
{
	private IViewFabric _fabric;
	private MenuInfinityPanelView _MenuInfinityPanelView;
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
        _MenuInfinityPanelView = _fabric.Init<MenuInfinityPanelView>(parent);
        _MenuInfinityPanelView.SetData(_scoreDataManager.GetScoreRewardDataPackage(), _scoreDataManager.GetRecordScore());
    }
}

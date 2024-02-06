using Zenject;
using UnityEngine;
using EventBus;
using UnityEngine.UI;

public class MenuInfinityPanelView : MenuTutorialPanelView
{
   
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
        _MenuInfinityPanelView.SetData(_scoreDataManager.GetScoreRewardDataPackage());
    }
}

using Zenject;
using UnityEngine;

public class NavigationCanvasView : MonoBehaviour
{

}

public class NavigationCanvasViewService : IService
{
	private IViewFabric _fabric;
	private IServiceFabric _serviceFabric;
	private NavigationCanvasView _NavigationCanvasView;
    private IMarkerService _markerService;

	private NavigationPanelViewService _navigationPanelViewService;
    private MenuCoinPanelViewService _menuCoinPanelViewService;
	private BackButtonViewService _backButtonViewService;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService, IServiceFabric serviceFabric)
	{
        _serviceFabric = serviceFabric;
        _markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _NavigationCanvasView = _fabric.Init<NavigationCanvasView>();
        _navigationPanelViewService = _serviceFabric.Init<NavigationPanelViewService>();
        _menuCoinPanelViewService = _serviceFabric.Init<MenuCoinPanelViewService>();
		_backButtonViewService = _serviceFabric.Init<BackButtonViewService>();

        _navigationPanelViewService.ActivateService();
        _menuCoinPanelViewService.ActivateService();
		_backButtonViewService.ActivateService();
    }

	public void HideBackButton()
	{
        _backButtonViewService.HideView();
    }

	public void ShowBackButton()
	{
		_backButtonViewService.ShowView();
	}
}

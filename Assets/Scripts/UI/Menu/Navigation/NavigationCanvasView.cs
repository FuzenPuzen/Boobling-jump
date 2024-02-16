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
	private SettingButtonViewService _settingButtonViewService;

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
        _navigationPanelViewService = _serviceFabric.InitSingle<NavigationPanelViewService>();
        _menuCoinPanelViewService = _serviceFabric.InitSingle<MenuCoinPanelViewService>();
		_backButtonViewService = _serviceFabric.InitSingle<BackButtonViewService>();
		_settingButtonViewService = _serviceFabric.InitSingle<SettingButtonViewService>();
       
        _navigationPanelViewService.ActivateService();
        _settingButtonViewService.ActivateService();
        _menuCoinPanelViewService.ActivateService();
		_backButtonViewService.ActivateService();
    }

	public void SetPanelName(PanelName panelName)
	{
		_navigationPanelViewService.SetPanelName(panelName);
    }

	public void HideBackButton()
	{
        _backButtonViewService.HideView();
    }	

	public void ShowBackButton()
	{
		_backButtonViewService.ShowView();
	}

    public void HideSettingButton()
    {
        _settingButtonViewService.HideView();
    }

	public void ShowSettingButton()
    {
        _settingButtonViewService.ShowView();
    }
}

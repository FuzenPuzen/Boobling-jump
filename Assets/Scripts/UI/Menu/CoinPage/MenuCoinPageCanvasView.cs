using Zenject;
using UnityEngine;
using TMPro;

public class MenuCoinPageCanvasView : MonoBehaviour
{


}

public class MenuCoinPageCanvasViewService : IService
{
	private IViewFabric _fabric;
	private IServiceFabric _servicefabric;
	private MenuCoinPageCanvasView _MenuCoinPageCanvasView;
	private MenuCoinPanelViewService _menuCoinPanelViewService;


    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService, IServiceFabric servicefabric)
	{
		_servicefabric = servicefabric;
		_fabric = fabric;
    }

	public void ActivateService()
	{
        _MenuCoinPageCanvasView = _fabric.Init<MenuCoinPageCanvasView>();
        _menuCoinPanelViewService = _servicefabric.Init<MenuCoinPanelViewService>();
		_menuCoinPanelViewService.ActivateService();

    }
}

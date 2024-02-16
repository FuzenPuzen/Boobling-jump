using Zenject;
using UnityEngine;

public class SettingCanvasView : MonoBehaviour
{

}

public class SettingCanvasViewService : IService
{
	private IViewFabric _fabric;
	private IServiceFabric _serviceFabric;
	private SettingCanvasView _SettingCanvasView;
    private SettingsPanelViewService settingsPanelViewService;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService, IServiceFabric serviceFabric)
	{
        _serviceFabric = serviceFabric;
        _markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _SettingCanvasView = _fabric.Init<SettingCanvasView>();
        settingsPanelViewService = _serviceFabric.InitSingle<SettingsPanelViewService>();
        settingsPanelViewService.ActivateService();
    }
}

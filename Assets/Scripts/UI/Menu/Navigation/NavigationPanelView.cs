using Zenject;
using UnityEngine;

public class NavigationPanelView : MonoBehaviour
{

}

public class NavigationPanelViewService : IService
{
	private IViewFabric _fabric;
	private NavigationPanelView _NavigationPanelView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
		Transform parent = _markerService.GetTransformMarker<NavigationCanvasMarker>().transform;
        _NavigationPanelView = _fabric.Init<NavigationPanelView>(parent);
	}
}

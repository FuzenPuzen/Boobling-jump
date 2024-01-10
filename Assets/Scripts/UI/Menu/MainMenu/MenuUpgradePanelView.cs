using Zenject;
using UnityEngine;

public class MenuUpgradePanelView : MonoBehaviour
{

}

public class MenuUpgradePanelViewService : IService
{
	private IViewFabric _fabric;
    private MenuUpgradePanelView _menuUpgradePanelView;
    private IMarkerService _markerService;

    [Inject]
    public void Constructor(IViewFabric fabric, IMarkerService markerService)
    {
        _markerService = markerService;
        _fabric = fabric;
    }

    public void ActivateService()
    {
        _menuUpgradePanelView = _fabric.SpawnObject<MenuUpgradePanelView>(_markerService.GetTransformMarker<MenuMainPageMarker>().transform);
    }
}

using Zenject;
using UnityEngine;

public class MenuUpgradePageCanvasView : MonoBehaviour
{

}

public class MenuUpgradePageCanvasViewService : IService
{
    private MenuUpgradePageCanvasView _menuUpgradePageView;
    private IViewFabric _fabric;
    private IMarkerService _markerService;


    [Inject]
    public void Constructor(IViewFabric fabric, IMarkerService markerService)
    {
        _markerService = markerService;
        _fabric = fabric;
    }

    public void ActivateService()
    {
        _menuUpgradePageView = _fabric.SpawnObject<MenuUpgradePageCanvasView>();
    }
}

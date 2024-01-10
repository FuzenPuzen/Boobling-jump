using Zenject;
using UnityEngine;

public class MenuTutorialPanelView : MonoBehaviour
{

}

public class MenuTutorialPanelViewService : IService
{
    private IViewFabric _fabric;
    private MenuTutorialPanelView _menuTutorialShopPanelView;
    private IMarkerService _markerService;

    [Inject]
    public void Constructor(IViewFabric fabric, IMarkerService markerService)
    {
        _markerService = markerService;
        _fabric = fabric;
    }

    public void ActivateService()
    {
        _menuTutorialShopPanelView = _fabric.SpawnObject<MenuTutorialPanelView>(_markerService.GetTransformMarker<MenuMainPageMarker>().transform);
    }
}

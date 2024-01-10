using Zenject;
using UnityEngine;

public class MenuSkinShopPanelView : MonoBehaviour
{

}

public class MenuSkinShopPanelViewService : IService
{
    private IViewFabric _fabric;
    private MenuSkinShopPanelView _menuSkinShopPanelView;
    private IMarkerService _markerService;

    [Inject]
    public void Constructor(IViewFabric fabric, IMarkerService markerService)
    {
        _markerService = markerService;
        _fabric = fabric;
    }

    public void ActivateService()
    {
        _menuSkinShopPanelView = _fabric.SpawnObject<MenuSkinShopPanelView>(_markerService.GetTransformMarker<MenuMainPageMarker>().transform);
    }
}

using Zenject;
using UnityEngine;
using UnityEngine.UI;
using EventBus;

public class MenuSkinShopPanelView : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        EventBus<OnClickSkinShop>.Raise();
    }
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
        _menuSkinShopPanelView = _fabric.Init<MenuSkinShopPanelView>(_markerService.GetTransformMarker<MenuMainPageMarker>().transform);
    }
}

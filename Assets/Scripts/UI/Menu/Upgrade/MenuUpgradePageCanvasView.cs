using Zenject;
using UnityEngine;

public class MenuUpgradePageCanvasView : MonoBehaviour
{
    internal void HideView()
    {
        gameObject.SetActive(false);
    }

    internal void ShowView()
    {
        gameObject.SetActive(true);
    }
}

public class MenuUpgradePageCanvasViewService : IService
{
    private MenuUpgradePageCanvasView _menuUpgradePageView;
    private IService _superJumpUpgradePanelViewService;
    private IService _rollUpgradePanelViewService;
    private IViewFabric _fabric;
    private IServiceFabric _serviceFabric;
    private IMarkerService _markerService;


    [Inject]
    public void Constructor(IViewFabric fabric, IMarkerService markerService, IServiceFabric serviceFabric)
    {
        _markerService = markerService;
        _serviceFabric = serviceFabric;
        _fabric = fabric;
    }

    public void ActivateService()
    {
        _superJumpUpgradePanelViewService = _serviceFabric.Init<SuperJumpUpgradePanelViewService>();
        _rollUpgradePanelViewService = _serviceFabric.Init<RollUpgradePanelViewService>();

        _menuUpgradePageView = _fabric.Init<MenuUpgradePageCanvasView>();
        _superJumpUpgradePanelViewService.ActivateService();
        _rollUpgradePanelViewService.ActivateService();
        HideView();
    }

    public void HideView()
    {
        _menuUpgradePageView.HideView();
    }
    public void ShowView()
    {
        _menuUpgradePageView.ShowView();
    }
}

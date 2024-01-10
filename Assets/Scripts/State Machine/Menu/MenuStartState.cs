using System.Runtime.CompilerServices;
using Zenject;

public class MenuStartState : IBaseState
{
    private IService _menuMainPageViewService;
    private IService _menuTutorialPanelViewService;
    private IService _menuSkinShopPanelViewService;
    private IService _menuUpgradePanelViewService;
    private IMarkerService _markerService;
            
    [Inject]
    public void Constructor(MenuMainPageViewService menuMainPageViewService,
                            MenuTutorialPanelViewService menuTutorialPanelViewService,
                            MenuSkinShopPanelViewService menuSkinShopPanelViewService,
                            MenuUpgradePanelViewService menuUpgradePanelViewService,
                            IMarkerService markerService)
    {
        _markerService = markerService;
        _menuMainPageViewService = menuMainPageViewService;
        _menuSkinShopPanelViewService = menuSkinShopPanelViewService;
        _menuTutorialPanelViewService = menuTutorialPanelViewService;
        _menuUpgradePanelViewService = menuUpgradePanelViewService;
    }

    public void Enter()
    {
        _markerService.ActivateService();
        _menuMainPageViewService.ActivateService();
        _menuTutorialPanelViewService.ActivateService();
        _menuUpgradePanelViewService.ActivateService();
        _menuSkinShopPanelViewService.ActivateService();
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}

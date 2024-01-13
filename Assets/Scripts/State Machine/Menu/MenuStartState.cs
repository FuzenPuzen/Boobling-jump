using Zenject;

public class MenuStartState : IBaseState
{
    private MenuStateMachine _stateMachine;
    private IService _menuMainPageViewService;
    private IService _menuTutorialPanelViewService;
    private IService _menuSkinShopPanelViewService;
    private IService _menuUpgradePanelViewService;
    private IService _menuSkinShopPageCanvasViewService;
    private IMarkerService _markerService;
            
    [Inject]
    public void Constructor(MenuStateMachine stateMachine,
                            MenuMainPageCanvasViewService menuMainPageViewService,
                            MenuTutorialPanelViewService menuTutorialPanelViewService,
                            MenuSkinShopPanelViewService menuSkinShopPanelViewService,
                            MenuUpgradePanelViewService menuUpgradePanelViewService,
                            MenuSkinShopPageCanvasViewService menuSkinShopPageCanvasViewService,
                            IMarkerService markerService)
    {
        _stateMachine = stateMachine;
        _markerService = markerService;
        _menuSkinShopPageCanvasViewService = menuSkinShopPageCanvasViewService;
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
        _menuSkinShopPageCanvasViewService.ActivateService();
        _stateMachine.SetState<MenuMainPageState>();
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}

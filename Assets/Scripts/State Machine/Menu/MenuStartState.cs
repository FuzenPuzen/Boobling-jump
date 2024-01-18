using Zenject;

public class MenuStartState : IBaseState
{
    private MenuStateMachine _stateMachine;
    private IService _menuMainPageViewService;
    private IService _menuTutorialPanelViewService;
    private IService _menuSkinShopPanelViewService;
    private IService _menuUpgradePanelViewService;
    private IService _menuSkinShopPageCanvasViewService;
    private IService _menuUpgradePageCanvasViewService;
    private IService _superJumpUpgradePanelViewService;

    private IService _playerBehaviourDataManager;

    private IMarkerService _markerService;
            
    [Inject]
    public void Constructor(MenuStateMachine stateMachine,
                            MenuMainPageCanvasViewService menuMainPageViewService,
                            MenuTutorialPanelViewService menuTutorialPanelViewService,
                            MenuSkinShopPanelViewService menuSkinShopPanelViewService,
                            MenuUpgradePanelViewService menuUpgradePanelViewService,
                            MenuSkinShopPageCanvasViewService menuSkinShopPageCanvasViewService,
                            MenuUpgradePageCanvasViewService menuUpgradePageCanvasViewService,
                            SuperJumpUpgradePanelViewService superJumpUpgradePanelView,

                            IPlayerBehaviourDataManager playerBehaviourDataManager,
                            IMarkerService markerService)
    {
        _stateMachine = stateMachine;
        _markerService = markerService;
        _playerBehaviourDataManager = playerBehaviourDataManager;
        _menuUpgradePageCanvasViewService = menuUpgradePageCanvasViewService;
        _superJumpUpgradePanelViewService = superJumpUpgradePanelView;
        _menuSkinShopPageCanvasViewService = menuSkinShopPageCanvasViewService;
        _menuMainPageViewService = menuMainPageViewService;
        _menuSkinShopPanelViewService = menuSkinShopPanelViewService;
        _menuTutorialPanelViewService = menuTutorialPanelViewService;
        _menuUpgradePanelViewService = menuUpgradePanelViewService;
    }

    public void Enter()
    {
        _markerService.ActivateService();
        _playerBehaviourDataManager.ActivateService();
        _menuMainPageViewService.ActivateService();
        _menuTutorialPanelViewService.ActivateService();
        _menuUpgradePanelViewService.ActivateService();
        _menuSkinShopPanelViewService.ActivateService();
        //_menuSkinShopPageCanvasViewService.ActivateService();

        _menuUpgradePageCanvasViewService.ActivateService();
        _superJumpUpgradePanelViewService.ActivateService();

        _stateMachine.SetState<MenuMainPageState>();
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}

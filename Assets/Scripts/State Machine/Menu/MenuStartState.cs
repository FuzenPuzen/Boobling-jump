using Zenject;

public class MenuStartState : IBaseState
{
    private MenuStateMachine _stateMachine;

    private MenuMainPageCanvasViewService _menuMainPageViewService;
    private MenuSkinShopPageCanvasViewService _menuSkinShopPageCanvasViewService;
    private MenuUpgradePageCanvasViewService _menuUpgradePageCanvasViewService;

    private NavigationCanvasViewService _navigationCanvasViewService;

    private IService _playerBehaviourDataManager;
    private IPlayerSkinDataManager _playerSkinDataManager;

    private IMarkerService _markerService;
            
    [Inject]
    public void Constructor(MenuStateMachine stateMachine,

                            MenuMainPageCanvasViewService menuMainPageViewService,
                            MenuSkinShopPageCanvasViewService menuSkinShopPageCanvasViewService,
                            MenuUpgradePageCanvasViewService menuUpgradePageCanvasViewService,
                            IPlayerBehaviourDataManager playerBehaviourDataManager,
                            IMarkerService markerService,
                            IPlayerSkinDataManager playerSkinDataManager,
                            NavigationCanvasViewService navigationCanvasViewService)
    {
        _navigationCanvasViewService = navigationCanvasViewService;
        _playerSkinDataManager = playerSkinDataManager;
        _playerBehaviourDataManager = playerBehaviourDataManager;
        _menuSkinShopPageCanvasViewService = menuSkinShopPageCanvasViewService;
        _menuUpgradePageCanvasViewService = menuUpgradePageCanvasViewService;
        _menuMainPageViewService = menuMainPageViewService;
        _stateMachine = stateMachine;
        _markerService = markerService;
    }

    public void Enter()
    {
        _markerService.ActivateService();
        _playerBehaviourDataManager.ActivateService();
        _playerSkinDataManager.ActivateService();

        _navigationCanvasViewService.ActivateService();
        _menuMainPageViewService.ActivateService();
        _menuSkinShopPageCanvasViewService.ActivateService();
        _menuUpgradePageCanvasViewService.ActivateService();

        _stateMachine.SetState<MenuMainPageState>();
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}

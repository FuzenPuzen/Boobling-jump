using Zenject;

public class MenuStartState : IBaseState
{
    private MenuStateMachine _stateMachine;
    private BackButtonCanvasViewService _backButtonCanvasViewService;
    private MenuMainPageCanvasViewService _menuMainPageViewService;
    private MenuSkinShopPageCanvasViewService _menuSkinShopPageCanvasViewService;
    private MenuUpgradePageCanvasViewService _menuUpgradePageCanvasViewService;

    private IService _menuCoinPageCanvasViewService;
    private IService _playerBehaviourDataManager;
    private IPlayerSkinDataManager _playerSkinDataManager;

    private IMarkerService _markerService;
            
    [Inject]
    public void Constructor(MenuStateMachine stateMachine,
                            BackButtonCanvasViewService backButtonCanvasViewService,
                            MenuCoinPageCanvasViewService menuCoinPageCanvasViewService,
                            MenuMainPageCanvasViewService menuMainPageViewService,
                            MenuSkinShopPageCanvasViewService menuSkinShopPageCanvasViewService,
                            MenuUpgradePageCanvasViewService menuUpgradePageCanvasViewService,
                            IPlayerBehaviourDataManager playerBehaviourDataManager,
                            IMarkerService markerService,
                            IPlayerSkinDataManager playerSkinDataManager)
    {
        _playerSkinDataManager = playerSkinDataManager;
        _playerBehaviourDataManager = playerBehaviourDataManager;
        _menuSkinShopPageCanvasViewService = menuSkinShopPageCanvasViewService;
        _menuUpgradePageCanvasViewService = menuUpgradePageCanvasViewService;
        _menuMainPageViewService = menuMainPageViewService;
        _backButtonCanvasViewService = backButtonCanvasViewService;
        _stateMachine = stateMachine;
        _markerService = markerService;
        _menuCoinPageCanvasViewService = menuCoinPageCanvasViewService;
    }

    public void Enter()
    {
        _markerService.ActivateService();
        _playerBehaviourDataManager.ActivateService();
        _playerSkinDataManager.ActivateService();
        _backButtonCanvasViewService.ActivateService();
        _menuCoinPageCanvasViewService.ActivateService();
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

using Zenject;

public class MenuStartState : IBaseState
{
    private MenuStateMachine _stateMachine;
    private BackButtonCanvasViewService _backButtonCanvasViewService;

    private IService _menuCoinPageCanvasViewService;

    private IMarkerService _markerService;
            
    [Inject]
    public void Constructor(MenuStateMachine stateMachine,
                            BackButtonCanvasViewService backButtonCanvasViewService,
                            MenuCoinPageCanvasViewService menuCoinPageCanvasViewService,
                            IMarkerService markerService)
    {
        _backButtonCanvasViewService = backButtonCanvasViewService;
        _stateMachine = stateMachine;
        _markerService = markerService;
        _menuCoinPageCanvasViewService = menuCoinPageCanvasViewService;
    }

    public void Enter()
    {
        _markerService.ActivateService();
        _backButtonCanvasViewService.ActivateService();
        _menuCoinPageCanvasViewService.ActivateService();
        _stateMachine.SetState<MenuMainPageState>();
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}

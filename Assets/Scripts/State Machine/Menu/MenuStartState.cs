using Zenject;

public class MenuStartState : IBaseState
{
    private MenuStateMachine _stateMachine;

    private IService _menuCoinPageCanvasViewService;

    private IMarkerService _markerService;
            
    [Inject]
    public void Constructor(MenuStateMachine stateMachine,

                            MenuCoinPageCanvasViewService menuCoinPageCanvasViewService,
                            IMarkerService markerService)
    {
        _stateMachine = stateMachine;
        _markerService = markerService;
        _menuCoinPageCanvasViewService = menuCoinPageCanvasViewService;
        
    }

    public void Enter()
    {
        _markerService.ActivateService();
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

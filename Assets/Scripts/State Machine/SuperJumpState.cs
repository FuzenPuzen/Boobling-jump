using Zenject;

public class SuperJumpState : IBaseState
{
    private StateMachine _statemachine;
    private IPlayerBehaviorService _playerBehaviorService;

    [Inject]
    public void Constructor(IPlayerBehaviorService playerBehaviorService, StateMachine statemachine)
    {
        _playerBehaviorService = playerBehaviorService;
        _statemachine = statemachine;
    }

    public void Enter()
    {
        _playerBehaviorService.SetBehavior<PlayerSuperJumpBehavior>();
        _playerBehaviorService.SetBehaviorEndAction(OnBehavourEndAction);
    }

    public void Exit()
    {
        //do nothing
    }

    public void Update()
    {
        //do nothing
    }

    public void OnBehavourEndAction()
    {
        _statemachine.SetState<RollingState>();
    }
}

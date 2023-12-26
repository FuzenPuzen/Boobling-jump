using Zenject;

public class RollingState : IBaseState
{
    private StateMachine _statemachine;
    private IPlayerBehaviorService _playerBehaviorService;

    [Inject]
    public void Constructor(StateMachine statemachine, IPlayerBehaviorService playerBehaviorService)
    {
        _statemachine = statemachine;
        _playerBehaviorService = playerBehaviorService;
    }

    public void Enter()
    {
        _playerBehaviorService.SetBehavior<PlayerRollBehavior>();
        _playerBehaviorService.SetActionEndBehavior(OnBehavourEnd);
    }

    public void Exit()
    {
        //do nothing
    }

    public void Update()
    {
        //do nothing
    }

    public void OnBehavourEnd()
    {
        _statemachine.SetState<GameState>();
    }
}

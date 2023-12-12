using Zenject;

public class RollingState : IBaseState
{
    private StateMachine _statemachine;
    private BasicGameState _basicGameState;
    private IPlayerBehaviorService _playerBehaviorService;

    [Inject]
    public void Constructor(StateMachine statemachine, BasicGameState basicGameState, IPlayerBehaviorService playerBehaviorService)
    {
        _basicGameState = basicGameState;
        _statemachine = statemachine;
        _playerBehaviorService = playerBehaviorService;
    }

    public void Enter()
    {
        //_playerBehaviorService.SetBehavior<PlayerRollBehavior>();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}

using Zenject;

public class BasicGameState : IBaseState
{
    private StateMachine _statemachine;
    private IPlayerBehaviorService _playerBehaviorService;

    [Inject]
    public void Constructor(StateMachine statemachine,
                          IPlayerBehaviorService playerBehaviorService)
    {
        _statemachine = statemachine;
        _playerBehaviorService = playerBehaviorService;
    }

    public void Enter()
    {
        //_playerBehaviorService.SetBehavior<PlayerBasicJumpBehaviour>();
        _statemachine.SetState<SuperJumpState>();
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

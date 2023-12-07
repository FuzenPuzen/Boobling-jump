using Zenject;

public class BasicGameState : IBaseState
{
    private StateMachine _statemachine;
    private EndGameState _endGameState;
    private IPlayerBehaviorService _playerBehaviorService;

    [Inject]
    public void Constructor(StateMachine statemachine,
                          EndGameState endGameState,
                          IPlayerBehaviorService playerBehaviorService)
    {
        _statemachine = statemachine;
        _endGameState = endGameState;
        _playerBehaviorService = playerBehaviorService;
    }

    public void Enter()
    {
        _playerBehaviorService.SetBehavior<PlayerBasicJumpBehaviour>();
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

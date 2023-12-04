using Zenject;

public class BasicGameState : IBaseState
{
    private StateMachine _statemachine;
    private EndGameState _endGameState;
    private PlayerKitService _playerKitService;
    private IPlayerBehaviorService _playerBehaviorService;

    [Inject]
    public BasicGameState(StateMachine statemachine,
                          EndGameState endGameState,
                          PlayerKitService playerKitService,
                          IPlayerBehaviorService playerBehaviorService)
    {
        _statemachine = statemachine;
        _endGameState = endGameState;
        _playerKitService = playerKitService;
        _playerBehaviorService = playerBehaviorService;
    }

    public void Enter()
    {
        _playerKitService.SetActionOnPlayerDie(OnPlayerDie);
        _playerBehaviorService.SetBehavior<PlayerJumpBehavior>();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }

    public void OnPlayerDie()
    {
        if (_endGameState != null)
            _statemachine.SetState(_endGameState);
    }
}

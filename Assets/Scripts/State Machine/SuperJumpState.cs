using Zenject;

public class SuperJumpState : IBaseState
{
    private StateMachine _statemachine;
    private BasicGameState _basicGameState;
    private IPlayerBehaviorService _playerBehaviorService;

    [Inject]
    public void Constructor(IPlayerBehaviorService playerBehaviorService, StateMachine statemachine, BasicGameState basicGameState)
    {
        _playerBehaviorService = playerBehaviorService;
        _statemachine = statemachine;
        _basicGameState = basicGameState;
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
        _statemachine.SetState(_basicGameState);
    }
}

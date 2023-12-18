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
        _playerBehaviorService.SetBehavior<PlayerSimpleJumpBehaviour>();
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
        _statemachine.SetState<SuperJumpState>();
    }

}

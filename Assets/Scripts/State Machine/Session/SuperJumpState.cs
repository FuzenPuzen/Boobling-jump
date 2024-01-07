using Zenject;

public class SuperJumpState : IBaseState
{
    private StateMachine _statemachine;
    private IPlayerBehaviourService _playerBehaviourService;

    [Inject]
    public void Constructor(IPlayerBehaviourService playerBehaviourService, StateMachine statemachine)
    {
        _playerBehaviourService = playerBehaviourService;
        _statemachine = statemachine;
    }

    public void Enter()
    {
        _playerBehaviourService.SetBehaviour<PlayerSuperJumpBehaviour>();
        _playerBehaviourService.SetActionEndBehaviour(OnBehavourEnd);
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
        _statemachine.SetState<RollingState>();
    }
}

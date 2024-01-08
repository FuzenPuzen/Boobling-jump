using Zenject;

public class RollingState : IBaseState
{
    private StateMachine _statemachine;
    private IPlayerBehaviourService _playerBehaviourService;
    private ISectionBehavioursService _sectionBehaviourService;

    [Inject]
    public void Constructor(StateMachine statemachine, IPlayerBehaviourService playerBehaviourService,ISectionBehavioursService sectionBehaviourService)
    {
        _statemachine = statemachine;
        _playerBehaviourService = playerBehaviourService;
        _sectionBehaviourService = sectionBehaviourService;
    }

    public void Enter()
    {
        _playerBehaviourService.SetBehaviour<PlayerRollBehaviour>();
        _sectionBehaviourService.SetBehaviour<SectionRollBehaviour>();
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
        _statemachine.SetState<SuperJumpState>();
    }
}

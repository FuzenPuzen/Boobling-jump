using EventBus;
using Zenject;

public class RollingState : IBaseState
{
    private SessionStateMachine _statemachine;
    private IPlayerBehaviourService _playerBehaviourService;
    private ISectionBehavioursService _sectionBehaviourService;

    private EventBinding<OnRollDeactivate> _onRollDeactivate;

    [Inject]
    public void Constructor(SessionStateMachine statemachine, IPlayerBehaviourService playerBehaviourService,ISectionBehavioursService sectionBehaviourService)
    {
        _statemachine = statemachine;
        _playerBehaviourService = playerBehaviourService;
        _sectionBehaviourService = sectionBehaviourService;
    }

    public void Enter()
    {
        _playerBehaviourService.SetBehaviour<PlayerRollBehaviour>();
        _sectionBehaviourService.SetBehaviour<SectionRollBehaviour>();
        _onRollDeactivate = new (OnRollBehaviourEnd);
    }

    public void Exit()
    {
        _onRollDeactivate.Remove(OnRollBehaviourEnd);
    }

    public void Update()
    {
        //do nothing
    }

    public void OnRollBehaviourEnd()
    {
        _statemachine.SetState<GameState>();
    }
}

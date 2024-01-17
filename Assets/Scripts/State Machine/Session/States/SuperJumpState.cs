using EventBus;
using Zenject;

public class SuperJumpState : IBaseState
{

    private SessionStateMachine _statemachine;
    private IPlayerBehaviourService _playerBehaviourService;
    private ISectionBehavioursService _sectionBehaviourService;

    private EventBinding<OnSupperJumpDeactivate> _onSupperJumpDeactivate;

    [Inject]
    public void Constructor(
        IPlayerBehaviourService playerBehaviourService,
        SessionStateMachine statemachine,
        ISectionBehavioursService sectionBehavioursService)
    {
        _sectionBehaviourService = sectionBehavioursService;
        _playerBehaviourService = playerBehaviourService;
        _statemachine = statemachine;
    }

    public void Enter()
    {
        _playerBehaviourService.SetBehaviour<PlayerSuperJumpBehaviour>();
        _sectionBehaviourService.SetBehaviour<SectionSuperJumpBehaviour>();
        _onSupperJumpDeactivate = new (OnSuperJumpBehaviourEnd);
        //_playerBehaviourService.SetActionEndBehaviour(OnBehavourEnd);
    }

    public void Exit()
    {
        //do nothing
    }

    public void Update()
    {
        //do nothing
    }
    public void OnSuperJumpBehaviourEnd()
    {
        _statemachine.SetState<GameState>();
    }
}

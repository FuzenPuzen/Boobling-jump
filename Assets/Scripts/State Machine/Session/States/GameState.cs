using EventBus;
using Zenject;

public class GameState : IBaseState
{
    private SessionStateMachine _statemachine;
    private IPlayerBehaviourService _playerBehaviourService;
    private ISectionBehavioursService _sectionBehaviourService;

    private EventBinding<OnRollActivate> _onRollActivate;
    private EventBinding<OnSupperJumpActivate> _onSupperJumpActivate;

    [Inject]
    public void Constructor(SessionStateMachine statemachine,
                          IPlayerBehaviourService playerBehaviourService,
                          ISectionBehavioursService sectionBehaviourService)
    {
        _sectionBehaviourService = sectionBehaviourService;
        _statemachine = statemachine;
        _playerBehaviourService = playerBehaviourService;
    }

    public void Enter()
    {
        _playerBehaviourService.SetBehaviour<PlayerSimpleJumpBehaviour>();
        _sectionBehaviourService.SetBehaviour<SectionSimpleJumpBehaviour>();
        _onRollActivate = new EventBinding<OnRollActivate>(OnRollBehaviourStart);
        _onSupperJumpActivate = new EventBinding<OnSupperJumpActivate>(OnSuperJumpBehaviourStart);
    }

    public void Exit()
    {
        //do nothing
    }

    public void Update()
    {
        //do nothing
    }

    public void OnRollBehaviourStart()
    {
        _statemachine.SetState<RollingState>();
    }

    public void OnSuperJumpBehaviourStart()
    {
        _statemachine.SetState<SuperJumpState>();
    }
}

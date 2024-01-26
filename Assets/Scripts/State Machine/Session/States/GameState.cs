using EventBus;
using Unity.VisualScripting;
using Zenject;

public class GameState : IBaseState
{
    private SessionStateMachine _statemachine;
    private IPlayerBehaviourService _playerBehaviourService;
    private ISectionBehavioursService _sectionBehaviourService;

    private EventBinding<OnRollActivate> _onRollActivate;
    private EventBinding<OnSupperJumpActivate> _onSupperJumpActivate;
    private EventBinding<OnPlayerDie> _onPlayerDie;

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
        _onRollActivate = new (OnRollBehaviourStart);
        _onSupperJumpActivate = new (OnSuperJumpBehaviourStart);
        _onPlayerDie = new (OnPlayerDie);
    }

    public void Exit()
    {
        _onRollActivate.Remove(OnRollBehaviourStart);
        _onSupperJumpActivate.Remove(OnSuperJumpBehaviourStart);
        _onPlayerDie.Remove(OnPlayerDie);
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

    public void OnPlayerDie()
    {
        _statemachine.SetState<EndGameState>();
    }
}

using Zenject;

public class GameState : IBaseState
{
    private SessionStateMachine _statemachine;
    private IPlayerBehaviourService _playerBehaviourService;
    private ISectionBehavioursService _sectionBehaviourService;

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

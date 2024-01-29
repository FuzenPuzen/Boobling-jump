using EventBus;
using Zenject;

public class RollingState : IBaseState
{
    private SessionStateMachine _statemachine;
    private IPlayerBehaviourService _playerBehaviourService;
    private ISectionBehavioursService _sectionBehaviourService;

    private EventBinding<OnRollDeactivate> _onRollDeactivate;
    private PlayerStoolDestroyerService _playerStoolDestroyerService;

    [Inject]
    public void Constructor(SessionStateMachine statemachine,
                            IPlayerBehaviourService playerBehaviourService,
                            ISectionBehavioursService sectionBehaviourService,
                            PlayerStoolDestroyerService playerStoolDestroyerService)
    {
        _statemachine = statemachine;
        _playerStoolDestroyerService = playerStoolDestroyerService;
        _playerBehaviourService = playerBehaviourService;
        _sectionBehaviourService = sectionBehaviourService;
    }

    public void Enter()
    {
        _playerBehaviourService.SetBehaviour<PlayerRollBehaviour>();
        _sectionBehaviourService.SetBehaviour<SectionRollBehaviour>();
        _playerStoolDestroyerService.ShowView();
        _onRollDeactivate = new (OnRollBehaviourEnd);
    }

    public void Exit()
    {
        _playerStoolDestroyerService.HideView();
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

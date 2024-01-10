using Zenject;

public class PreStartState : IBaseState
{
    private SessionStateMachine _statemachine;
    private ITimerService _timerService;
    private TutorialPanelService _tutorialService;
    private IService _coinsPanelService;
    private IPlayerBehaviourService _playerBehaviourService;
    private ISectionBehavioursService _sectionBehaviourService;
    private IMarkerService _markerService;
    private IService _roomViewService;

    [Inject]
    public void Constructor(
                    ITimerService timerService,
                    SessionStateMachine statemachine,
                    TutorialPanelService tutorialService,
                    IPlayerBehaviourService playerBehaviourService,
                    ISectionBehavioursService sectionBehaviourService,
                    CoinsPanelService coinsPanelService,
                    IMarkerService  markerService,
                    RoomViewService roomViewService
                 )
    {
        _roomViewService = roomViewService;
        _sectionBehaviourService = sectionBehaviourService;
        _coinsPanelService = coinsPanelService;
        _playerBehaviourService = playerBehaviourService;
        _tutorialService = tutorialService;
        _timerService = timerService;
        _statemachine = statemachine;
        _markerService =  markerService;
    }
    public void Enter()
    {
        _markerService.ActivateService();
        _roomViewService.ActivateService();
        _sectionBehaviourService.ActivateService();
        _playerBehaviourService.ActivateService();
        _playerBehaviourService.SetBehaviour<PlayerStartBehaviour>();
        _playerBehaviourService.SetActionEndBehaviour(OnBehavourEnd);

        _tutorialService.ActivateService();
        _timerService.ActivateService();
        _coinsPanelService.ActivateService();
    }

    public void Exit()
    {
        _tutorialService.DeActivateService();
    }

    public void Update()
    {
        //do nothing
    }

    public void OnBehavourEnd()
    {
        _statemachine.SetState<StartState>();
    }
}

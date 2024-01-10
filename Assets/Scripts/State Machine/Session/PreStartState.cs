using Zenject;

public class PreStartState : IBaseState
{
    private StateMachine _statemachine;
    private ITimerService _timerService;
    private TutorialPanelService _tutorialService;
    private IService _coinsPanelService;
    private IPlayerBehaviourService _playerBehaviourService;
    private ISectionBehavioursService _sectionBehaviourService;
    private IMarkerService _markerService;
    private IService _roomViewService;
    private GiftCollectViewService _giftCollectViewService;
    private CoinPalleteViewService _coinPalleteViewService;

    [Inject]
    public void Constructor(
                    ITimerService timerService,
                    StateMachine statemachine,
                    TutorialPanelService tutorialService,
                    IPlayerBehaviourService playerBehaviourService,
                    ISectionBehavioursService sectionBehaviourService,
                    CoinsPanelService coinsPanelService,
                    IMarkerService  markerService,
                    RoomViewService roomViewService,
                    CoinPalleteViewService coinPalleteViewService,
                    GiftCollectViewService giftCollectViewService
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
        _giftCollectViewService = giftCollectViewService;
        _coinPalleteViewService = coinPalleteViewService;
    }
    public void Enter()
    {
        _markerService.ActivateService();
        
        _roomViewService.ActivateService();
        _sectionBehaviourService.ActivateService();
        _playerBehaviourService.ActivateService();
        _playerBehaviourService.SetBehaviour<PlayerStartBehaviour>();
        _playerBehaviourService.SetActionEndBehaviour(OnBehavourEnd);
        _giftCollectViewService.ActivateService();
        _tutorialService.ActivateService();
        _timerService.ActivateService();
        _coinsPanelService.ActivateService();
        _coinPalleteViewService.ActivateService();
        
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

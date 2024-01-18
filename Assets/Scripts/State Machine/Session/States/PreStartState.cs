using EventBus;
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
    private IPoolsViewService _poolsViewService;
    private GiftCollectorViewService _giftCollectViewService;
    private CoinCollectorViewService _coinCollectViewService;
    private CoinPalleteViewService _coinPalleteViewService;
    private RollBonusBlenderViewService _rollBonusBlenderViewService;
    private SuperJumpBonusBlenderViewService _superJumpBonusBlenderViewService;
    private BlackBoardViewService _blackBoardViewService;
    private EventBinding<OnStartBehaviourEnd> _onStartBehaviourEnd;


    [Inject]
    public void Constructor(
                    ITimerService timerService,
                    SessionStateMachine statemachine,
                    TutorialPanelService tutorialService,
                    IPlayerBehaviourService playerBehaviourService,
                    ISectionBehavioursService sectionBehaviourService,
                    CoinsPanelService coinsPanelService,
                    IMarkerService  markerService,
                    RoomViewService roomViewService,
                    CoinPalleteViewService coinPalleteViewService,
                    GiftCollectorViewService giftCollectViewService,
                    CoinCollectorViewService coinCollectViewService,
                    IPoolsViewService poolsViewService,
                    RollBonusBlenderViewService rollBonusBlenderViewService,
                    SuperJumpBonusBlenderViewService superJumpBonusBlenderViewService,
                    BlackBoardViewService blackBoardViewService
                 )
    {
        _poolsViewService = poolsViewService;
        _roomViewService = roomViewService;
        _sectionBehaviourService = sectionBehaviourService;
        _coinsPanelService = coinsPanelService;
        _playerBehaviourService = playerBehaviourService;
        _tutorialService = tutorialService;
        _timerService = timerService;
        _statemachine = statemachine;
        _markerService =  markerService;
        _giftCollectViewService = giftCollectViewService;
        _coinCollectViewService = coinCollectViewService;
        _coinPalleteViewService = coinPalleteViewService;
        _rollBonusBlenderViewService = rollBonusBlenderViewService;
        _superJumpBonusBlenderViewService = superJumpBonusBlenderViewService;
        _blackBoardViewService = blackBoardViewService;
    }
    public void Enter()
    {
        _markerService.ActivateService();

        _roomViewService.ActivateService();
        _blackBoardViewService.ActivateService();
        _sectionBehaviourService.ActivateService();
        _rollBonusBlenderViewService.ActivateService();
        _superJumpBonusBlenderViewService.ActivateService();

        _playerBehaviourService.ActivateService();
        _playerBehaviourService.SetBehaviour<PlayerStartBehaviour>();
        _poolsViewService.ActivateService();

        _giftCollectViewService.ActivateService();
        _coinCollectViewService.ActivateService();
        _tutorialService.ActivateService();
        _timerService.ActivateService();
        _coinsPanelService.ActivateService();
        _coinPalleteViewService.ActivateService();

        _onStartBehaviourEnd = new(OnStartBehaviourEnd);

    }

    public void Exit()
    {
        _tutorialService.DeActivateService();
    }

    public void Update()
    {
        //do nothing
    }

    public void OnStartBehaviourEnd()
    {
        _statemachine.SetState<StartState>();
    }

}

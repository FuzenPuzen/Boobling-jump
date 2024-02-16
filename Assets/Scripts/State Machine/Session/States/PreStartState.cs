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
    private ILoaderSceneService _loaderSceneService;
    private IService _roomViewService;
    private IPoolsViewService _poolsViewService;
    private GiftCollectorViewService _giftCollectViewService;
    private CoinCollectorViewService _coinCollectViewService;
    private CoinPalleteViewService _coinPalleteViewService;
    private RollBonusBlenderViewService _rollBonusBlenderViewService;
    private SuperJumpBonusBlenderViewService _superJumpBonusBlenderViewService;
    private BlackBoardViewService _blackBoardViewService;
    private EventBinding<OnStartBehaviourEnd> _onStartBehaviourEnd;
    private IPlayerSkinDataManager _playerSkinDataManager;
    private IService _playerBehaviourDataManager;
    private PlayerStoolDestroyerService _playerBonusDestroyerService;
    private SuperJumpWavesService _superJumpWavesService;
    private IRepaintService _repaintService;
    private MainCameraViewService _mainCameraViewService;
    private IAudioService _audioService;
    private IAnimationService _animationService;

    [Inject]
    public void Constructor(
                    ITimerService timerService,
                    SessionStateMachine statemachine,
                    TutorialPanelService tutorialService,
                    IPlayerBehaviourService playerBehaviourService,
                    ISectionBehavioursService sectionBehaviourService,
                    CoinsPanelService coinsPanelService,
                    IMarkerService  markerService,
                    ILoaderSceneService loaderSceneService,
                    RoomViewService roomViewService,
                    CoinPalleteViewService coinPalleteViewService,
                    GiftCollectorViewService giftCollectViewService,
                    CoinCollectorViewService coinCollectViewService,
                    IPoolsViewService poolsViewService,
                    RollBonusBlenderViewService rollBonusBlenderViewService,
                    SuperJumpBonusBlenderViewService superJumpBonusBlenderViewService,
                    BlackBoardViewService blackBoardViewService,
                    IPlayerSkinDataManager playerSkinDataManager,
                    IPlayerBehaviourDataManager playerBehaviourDataManager,
                    PlayerStoolDestroyerService playerBonusDestroyerService,
                    SuperJumpWavesService superJumpWavesService,
                    IRepaintService repaintService,
                    MainCameraViewService mainCameraViewService,
                    IAudioService audioService,
                    IAnimationService animationService)
    {
        _superJumpWavesService = superJumpWavesService;
        _playerBonusDestroyerService = playerBonusDestroyerService;
        _playerBehaviourDataManager = playerBehaviourDataManager;
        _playerSkinDataManager = playerSkinDataManager;
        _poolsViewService = poolsViewService;
        _roomViewService = roomViewService;
        _sectionBehaviourService = sectionBehaviourService;
        _coinsPanelService = coinsPanelService;
        _playerBehaviourService = playerBehaviourService;
        _tutorialService = tutorialService;
        _timerService = timerService;
        _statemachine = statemachine;
        _markerService =  markerService;
        _loaderSceneService = loaderSceneService;
        _giftCollectViewService = giftCollectViewService;
        _coinCollectViewService = coinCollectViewService;
        _coinPalleteViewService = coinPalleteViewService;
        _rollBonusBlenderViewService = rollBonusBlenderViewService;
        _superJumpBonusBlenderViewService = superJumpBonusBlenderViewService;
        _blackBoardViewService = blackBoardViewService;
        _repaintService = repaintService;
        _mainCameraViewService = mainCameraViewService;
        _audioService = audioService;
        _animationService = animationService;
    }

    public void Enter()
    {
        _markerService.ActivateService(); 
        _poolsViewService.ActivateService();

        _repaintService.ActivateService();
        _loaderSceneService.ActivateService();

        _playerBehaviourDataManager.ActivateService();
        _playerSkinDataManager.ActivateService();

        _mainCameraViewService.ActivateService();
        _roomViewService.ActivateService();
        _blackBoardViewService.ActivateService();
        _sectionBehaviourService.ActivateService();
        _rollBonusBlenderViewService.ActivateService();
        _superJumpBonusBlenderViewService.ActivateService();

        _playerBehaviourService.ActivateService();
        _playerBehaviourService.SetBehaviour<PlayerStartBehaviour>();
        
        _audioService.ActivateService();
        _animationService.ActivateService();

        _playerBonusDestroyerService.ActivateService();
        _superJumpWavesService.ActivateService();
        _giftCollectViewService.ActivateService();
        _coinCollectViewService.ActivateService();
        _tutorialService.ActivateService();
        _timerService.ActivateService();
        _coinsPanelService.ActivateService();
        _coinPalleteViewService.ActivateService();
        _onStartBehaviourEnd = new (OnStartBehaviourEnd);
    }

    public void Exit()
    {
        _tutorialService.DeActivateService();
        _onStartBehaviourEnd.Remove(OnStartBehaviourEnd);
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

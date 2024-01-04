using Zenject;

public class StartState : IBaseState
{
    private StateMachine _statemachine;
    private IService _sectionsService;
    private IService _currentScoreService;
    private IService _recordScoreService;
    private IService _giftScoreService;
    private IService _coinPoolViewManager;
    private IService _giftService;


    [Inject]
    public void Constructor(
                        ISectionBehavioursService sectionsService,
                        StateMachine statemachine,
                        CurrentScorePanelService scoreService,
                        CoinPoolViewManager coinPoolViewManager,
                        RecordScorePanelService recordScoreService,
                        GiftScorePanelService giftScorePanelService,
                        GiftService giftService
                     )
    {
        _coinPoolViewManager = coinPoolViewManager;
        _currentScoreService = scoreService;
        _sectionsService = sectionsService;
        _statemachine = statemachine;
        _recordScoreService = recordScoreService;
        _giftScoreService = giftScorePanelService;
        _giftService = giftService;
    }

    public void Enter()
    {
        _currentScoreService.ActivateService();
        _recordScoreService.ActivateService();
        _giftScoreService.ActivateService();
        _sectionsService.ActivateService();
        _coinPoolViewManager.ActivateService();
        _giftService.ActivateService();
        _statemachine.SetState<GameState>();
    }

    public void Exit()
    {
        //do nothing
    }

    public void Update()
    {
        //do nothing
    }
  
}

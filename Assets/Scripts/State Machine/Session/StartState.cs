using Zenject;

public class StartState : IBaseState
{
    private StateMachine _statemachine;
    private Iservice _sectionsService;
    private Iservice _currentScoreService;
    private Iservice _recordScoreService;
    private Iservice _giftScoreService;
    private Iservice _coinsPoolService;
    private Iservice _giftService;


    [Inject]
    public void Constructor(
                        SectionsBehaviourService sectionsService,
                        StateMachine statemachine,
                        CurrentScorePanelService scoreService,
                        CoinsPoolService coinsPoolService,
                        RecordScorePanelService recordScoreService,
                        GiftScorePanelService giftScorePanelService,
                        GiftService giftService
                     )
    {
        _coinsPoolService = coinsPoolService;
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
        _coinsPoolService.ActivateService();
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

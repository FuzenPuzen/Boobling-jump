using Zenject;

public class StartState : IBaseState
{
    private StateMachine _statemachine;
    private Iservice _sectionsService;
    private Iservice _currentScoreService;
    private Iservice _recordScoreService;
    private Iservice _coinsPoolService;


    [Inject]
    public void Constructor(
                        SectionsService sectionsService,
                        StateMachine statemachine,
                        CurrentScorePanelService scoreService,
                        CoinsPoolService coinsPoolService,
                        RecordScorePanelService recordScoreService
                     )
    {
        _coinsPoolService = coinsPoolService;
        _currentScoreService = scoreService;
        _sectionsService = sectionsService;
        _statemachine = statemachine;
        _recordScoreService = recordScoreService;
    }

    public void Enter()
    {
        _currentScoreService.ActivateService();
        _recordScoreService.ActivateService();
        _sectionsService.ActivateService();
        _coinsPoolService.ActivateService();
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

using Zenject;

public class StartState : IBaseState
{
    private StateMachine _statemachine;
    private IService _sectionsService;
    private IService _currentScoreService;
    private IService _recordScoreService;
    private IService _coinsPoolService;


    [Inject]
    public void Constructor(
                        SectionsBehaviourService sectionsService,
                        StateMachine statemachine,
                        CurrentScorePanelService scoreService,
                        CoinPoolViewManager coinsPoolService,
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

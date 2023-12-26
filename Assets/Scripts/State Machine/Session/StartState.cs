using Zenject;

public class StartState : IBaseState
{
    private StateMachine _statemachine;
    private Iservice _sectionsService;
    private Iservice _scoreService;
    private Iservice _coinsPoolService;

    private Iservice _coinsService;

    [Inject]
    public void Constructor(
                        SectionsService sectionsService,
                        StateMachine statemachine,
                        ScoreService scoreService,
                        CoinsService coinsService,
                        CoinsPoolService coinsPoolService
                     )
    {
        _coinsPoolService = coinsPoolService;
        _coinsService = coinsService;
        _scoreService = scoreService;
        _sectionsService = sectionsService;
        _statemachine = statemachine;
    }

    public void Enter()
    {
        _scoreService.ActivateService();
        _sectionsService.ActivateService();
        _coinsPoolService.ActivateService();
        _coinsService.ActivateService();
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

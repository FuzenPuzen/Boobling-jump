using Zenject;

public class StartState : IBaseState
{
    private StateMachine _statemachine;
    private Iservice _sectionsService;
    private Iservice _scoreService;
    private IPlayerBehaviorService _playerBehaviorService;

    [Inject]
    public void Constructor(
                        SectionsService sectionsService,
                        StateMachine statemachine,
                        IPlayerBehaviorService playerBehaviorService,
                        ScoreService scoreService
                     )
    {
        _scoreService = scoreService;
        _playerBehaviorService = playerBehaviorService;
        _sectionsService = sectionsService;
        _statemachine = statemachine;
    }

    public void Enter()
    {
        _scoreService.ActivateService();
        _sectionsService.ActivateService();       
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

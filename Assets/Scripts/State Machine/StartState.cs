using Zenject;

public class StartState : IBaseState
{
    private StateMachine _statemachine;
    private Iservice _sectionsService;
    private ITimerService _timerService;
    private Iservice _tutorialService;
    private IPlayerBehaviorService _playerBehaviorService;

    [Inject]
    public void Constructor(
                        SectionsService sectionsService,
                        ITimerService timerService,
                        StateMachine statemachine,
                        TutorialService tutorialService,
                        IPlayerBehaviorService playerBehaviorService
                     )
    {
        _playerBehaviorService = playerBehaviorService;
        _tutorialService = tutorialService;
        _timerService = timerService;
        _sectionsService = sectionsService;
        _statemachine = statemachine;
    }

    public void Enter()
    {
        _playerBehaviorService.ActivateService();
        _tutorialService.ActivateService();
        _timerService.ActivateService();
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

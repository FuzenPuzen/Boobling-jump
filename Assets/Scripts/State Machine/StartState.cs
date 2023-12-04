using Zenject;

public class StartState : IBaseState
{
    private StateMachine _statemachine;
    private PlayerKitService _playerKitService;
    private Iservice _sectionsService;
    private ITimerService _timerService;
    private Iservice _tutorialService;

    private BasicGameState _gameState;

    [Inject]
    public StartState(
                        SectionsService sectionsService,
                        ITimerService timerService,
                        PlayerKitService playerKitService,
                        StateMachine statemachine,
                        TutorialService tutorialService,
                        BasicGameState gameState
                     )
    {
        _gameState = gameState;
        _tutorialService = tutorialService;
        _timerService = timerService;
        _sectionsService = sectionsService;
        _playerKitService = playerKitService;
        _statemachine = statemachine;
    }

    public void Enter()
    {
        _tutorialService.ActivateService();
        _timerService.ActivateService();
        _playerKitService.ActivateService();
        _sectionsService.ActivateService();       
        _statemachine.SetState(_gameState);
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

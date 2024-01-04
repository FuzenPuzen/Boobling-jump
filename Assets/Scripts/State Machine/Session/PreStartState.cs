using Zenject;

public class PreStartState : IBaseState
{
    private StateMachine _statemachine;
    private ITimerService _timerService;
    private TutorialPanelService _tutorialService;
    private IService _coinsPanelService;
    private IPlayerBehaviorService _playerBehaviorService;
    private ISectionBehaviorService _sectionBehaviorService;

    [Inject]
    public void Constructor(
                    ITimerService timerService,
                    StateMachine statemachine,
                    TutorialPanelService tutorialService,
                    IPlayerBehaviorService playerBehaviorService,
                    ISectionBehaviorService sectionBehaviorService,
                    CoinsPanelService coinsPanelService
                 )
    {
        _sectionBehaviorService = sectionBehaviorService;
        _coinsPanelService = coinsPanelService;
        _playerBehaviorService = playerBehaviorService;
        _tutorialService = tutorialService;
        _timerService = timerService;
        _statemachine = statemachine;
    }
    public void Enter()
    {
        _sectionBehaviorService.ActivateService();
        _playerBehaviorService.ActivateService();
        _playerBehaviorService.SetBehavior<PlayerStartBehaviour>();
        _playerBehaviorService.SetActionEndBehavior(OnBehavourEnd);

        _tutorialService.ActivateService();
        _timerService.ActivateService();
        _coinsPanelService.ActivateService();
    }

    public void Exit()
    {
        _tutorialService.DeActivateService();
    }

    public void Update()
    {
        //do nothing
    }

    public void OnBehavourEnd()
    {
        _statemachine.SetState<StartState>();
    }
}

using Zenject;

public class PreStartState : IBaseState
{
    private StateMachine _statemachine;
    private ITimerService _timerService;
    private TutorialService _tutorialService;
    private Iservice _coinsPanelService;
    private IPlayerBehaviorService _playerBehaviorService;

    [Inject]
    public void Constructor(
                    ITimerService timerService,
                    StateMachine statemachine,
                    TutorialService tutorialService,
                    IPlayerBehaviorService playerBehaviorService,
                    CoinsPanelService coinsPanelService
                 )
    {
        _coinsPanelService = coinsPanelService;
        _playerBehaviorService = playerBehaviorService;
        _tutorialService = tutorialService;
        _timerService = timerService;
        _statemachine = statemachine;
    }
    public void Enter()
    {
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

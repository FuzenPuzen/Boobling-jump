using UnityEngine;
using Zenject;

public class StartState : IBaseState
{
    private StateMachine _statemachine;
    private EndGameState _endGameState;
    private PlayerKitService _playerKitService;
    private Iservice _sectionsService;
    private ITimerService _timerService;
    private Iservice _tutorialService;

    [Inject]
    public StartState(
                        SectionsService sectionsService,
                        ITimerService timerService,
                        PlayerKitService playerKitService,
                        StateMachine statemachine,
                        EndGameState endGameState,
                        TutorialService tutorialService
                     )
    {
        _tutorialService = tutorialService;
        _timerService = timerService;
        _sectionsService = sectionsService;
        _playerKitService = playerKitService;
        _statemachine = statemachine;
        _endGameState = endGameState;
    }

    public void Enter()
    {
        _tutorialService.ActivateService();
        _timerService.ActivateService();
        _playerKitService.ActivateService();
        _sectionsService.ActivateService();
        _playerKitService.SetplayerDieAction(OnPlayerDie);
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }

    public void OnPlayerDie()
    {
        if (_endGameState != null)
            _statemachine.SetState(_endGameState);
    }
}

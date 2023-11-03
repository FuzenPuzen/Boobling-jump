using UnityEngine;
using Zenject;

public class StartState : IBaseState
{
    private StateMachine _statemachine;
    private EndGameState _endGameState;

    [Inject]
    public StartState(
                        SectionsService sectionsService,
                        ITimerService timerService,
                        PlayerKitService playerKitService,
                        StateMachine statemachine,
                        EndGameState endGameState)
    {
        _statemachine = statemachine;
        _endGameState = endGameState;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}

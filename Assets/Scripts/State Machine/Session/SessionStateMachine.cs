using System.Collections.Generic;
using System.Linq;
using Zenject;

public class SessionStateMachine 
{
    private IBaseState _currentState;

    private List<IBaseState> _baseStates = new();

    public void SetState<T>() where T : IBaseState
    { 
        _currentState?.Exit();
        _currentState = _baseStates.OfType<T>().FirstOrDefault();
        _currentState.Enter();
    }

    [Inject]
    public void Constructor(StartState startState,
                            GameState basicGameState,
                            SuperJumpState superJumpState,
                            RollingState rollingState,
                            EndGameState endGameState,
                            PreStartState preStartState,
                            TutorialFinishState tutorialFinishState,
                            SessionLastState sessionLastState)
    {
        _baseStates.Add(tutorialFinishState);
        _baseStates.Add(preStartState);
        _baseStates.Add(startState);
        _baseStates.Add(basicGameState);
        _baseStates.Add(superJumpState);
        _baseStates.Add(rollingState);
        _baseStates.Add(endGameState);
        _baseStates.Add(sessionLastState);
    }

    public void UpdateState()
    {
        _currentState?.Update();
    }
}

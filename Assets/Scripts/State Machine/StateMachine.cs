using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class StateMachine 
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
                            EndGameState endGameState)
    {
        _baseStates.Add(startState);
        _baseStates.Add(basicGameState);
        _baseStates.Add(superJumpState);
        _baseStates.Add(rollingState);
        _baseStates.Add(endGameState);
    }

    public void UpdateState()
    {
        _currentState?.Update();
    }
}

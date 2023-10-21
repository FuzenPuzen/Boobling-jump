using Zenject.Asteroids;

public class StateMachine 
{
    private IBaseState _currentState;

    public void SetState(IBaseState newState)
    {
        if (newState != _currentState)
        { 
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }

    public void UpdateState()
    {
        _currentState?.Update();
    }
}

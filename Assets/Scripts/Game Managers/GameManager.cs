using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private StateMachine _stateMachine;
    private StartState _startState;

    [Inject]
    private void Construct(StateMachine stateMachine, StartState startState)
    {
        _stateMachine = stateMachine;
        _startState = startState;
    }

    public void Start()
    {
        _stateMachine.SetState(_startState);
    }
}

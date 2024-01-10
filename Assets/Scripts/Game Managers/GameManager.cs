using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private SessionStateMachine _stateMachine;

    [Inject]
    private void Construct(SessionStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Start()
    {
        _stateMachine.SetState<PreStartState>();
    }
}

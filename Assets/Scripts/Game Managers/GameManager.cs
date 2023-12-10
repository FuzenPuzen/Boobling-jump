using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private StateMachine _stateMachine;

    [Inject]
    private void Construct(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Start()
    {
        _stateMachine.SetState<StartState>();
    }
}

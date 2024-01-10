using UnityEngine;
using Zenject;

public class MenuManager : MonoBehaviour
{
    private MenuStateMachine _stateMachine;

    [Inject]
    private void Construct(MenuStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Start()
    {
        _stateMachine.SetState<MenuStartState>();
    }
}

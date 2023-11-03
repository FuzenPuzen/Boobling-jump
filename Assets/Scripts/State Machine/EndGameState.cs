using UnityEngine;
using Zenject;

public class EndGameState : IBaseState
{

    [Inject]
    public EndGameState()
    {
        
    }

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}

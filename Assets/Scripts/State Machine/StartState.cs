using UnityEngine;
using Zenject;

public class StartState : IBaseState
{

    [Inject]
    public StartState(StoolsService stoolsService, TimerService timerService)
    {
        
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

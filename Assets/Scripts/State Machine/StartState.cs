using UnityEngine;
using Zenject;

public class StartState : IBaseState
{

    [Inject]
    public StartState(SectionsService sectionsService, TimerService timerService, PlayerKitService playerKitService)
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

using UnityEngine;
using Zenject;

public class StartState : IBaseState
{
    private SectionsService _sectionsService;

    [Inject]
    public StartState(SectionsService sectionsService)
    {
        _sectionsService = sectionsService;
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

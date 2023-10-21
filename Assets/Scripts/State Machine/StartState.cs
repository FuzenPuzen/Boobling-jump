using UnityEngine;
using Zenject;

public class StartState : IBaseState
{
    private PlayerFabric _playerFabric;
    private SectionsService _sectionsService;

    [Inject]
    public StartState(PlayerFabric playerFabric, SectionsService sectionsService)
    {
        _playerFabric = playerFabric;
        _sectionsService = sectionsService;
    }

    public void Enter()
    {
        _sectionsService.SetPlayer(_playerFabric.SpawnPlayer());
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}

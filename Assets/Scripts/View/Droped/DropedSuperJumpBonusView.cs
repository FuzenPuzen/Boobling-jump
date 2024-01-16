using Zenject;
using UnityEngine;
using System;

public class DropedSuperJumpBonusView : MonoBehaviour
{

}

public class DropedSuperJumpBonusViewService : IPoolingViewService
{
	private IViewFabric _fabric;
	private DropedSuperJumpBonusView _DropedSuperJumpBonusView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
        
    }

    public void ActivateServiceFromPool()
    {
        Vector3 spawnPos = _markerService.GetTransformMarker<PlayerMarker>().transform.position;
        Transform parent = _markerService.GetTransformMarker<PlayerMarker>().transform;
        _DropedSuperJumpBonusView = _fabric.SpawnObject<DropedSuperJumpBonusView>();

    }

    public void SetDeactivateAction(Action<IPoolingViewService> action)
    {
        
    }
}

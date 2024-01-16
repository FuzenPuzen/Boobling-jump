using Zenject;
using UnityEngine;
using System;

public class DropedRollBonusView : MonoBehaviour
{

}

public class DropedRollBonusViewService : IPoolingViewService
{
	private IViewFabric _fabric;
	private DropedRollBonusView _DropedRollBonusView;
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
        _DropedRollBonusView = _fabric.SpawnObject<DropedRollBonusView>();
    }

    public void SetDeactivateAction(Action<IPoolingViewService> action)
    {
        
    }
}

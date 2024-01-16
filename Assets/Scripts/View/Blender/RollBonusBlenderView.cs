using Zenject;
using UnityEngine;

public class RollBonusBlenderView : MonoBehaviour
{

}

public class RollBonusBlenderViewService : IService
{
	private IViewFabric _fabric;
	private RollBonusBlenderView _RollBonusBlenderView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _RollBonusBlenderView = _fabric.SpawnObject<RollBonusBlenderView>();
	}
}

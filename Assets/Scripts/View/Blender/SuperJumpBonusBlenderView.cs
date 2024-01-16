using Zenject;
using UnityEngine;

public class SuperJumpBonusBlenderView : MonoBehaviour
{

}

public class SuperJumpBonusBlenderViewService : IService
{
	private IViewFabric _fabric;
	private SuperJumpBonusBlenderView _SuperJumpBonusBlenderView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _SuperJumpBonusBlenderView = _fabric.SpawnObject<SuperJumpBonusBlenderView>();
	}
}

using Zenject;
using UnityEngine;

public class DropedCoinDeactivatorView : MonoBehaviour
{

}

public class DropedCoinDeactivatorViewService : IService
{
	private IViewFabric _fabric;
	private DropedCoinDeactivatorView _DropedCoinDeactivatorView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _DropedCoinDeactivatorView = _fabric.Init<DropedCoinDeactivatorView>();
	}
}

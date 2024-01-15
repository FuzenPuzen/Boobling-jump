using Zenject;
using UnityEngine;

public class SuperJumpUpgradePanelView : MonoBehaviour
{

}

public class SuperJumpUpgradePanelViewService : IService
{
	private IViewFabric _fabric;
	private SuperJumpUpgradePanelView _SuperJumpUpgradePanelView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _SuperJumpUpgradePanelView = _fabric.SpawnObject<SuperJumpUpgradePanelView>();
	}
}

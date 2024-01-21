using Zenject;
using UnityEngine;

public class PlayerSkinView : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(new(0, 2, 0));
    }
}

public class PlayerSkinViewService : IService
{
	private IViewFabric _fabric;
	private PlayerSkinView _PlayerSkinView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _PlayerSkinView = _fabric.Init<PlayerSkinView>();
	}
}

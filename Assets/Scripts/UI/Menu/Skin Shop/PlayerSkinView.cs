using Zenject;
using UnityEngine;

public class PlayerSkinView : MonoBehaviour
{
	[SerializeField] private float _speed = 2;
    private void FixedUpdate()
    {
        transform.Rotate(new(0, _speed, 0));
							
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

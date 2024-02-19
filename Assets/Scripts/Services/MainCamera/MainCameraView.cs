using Zenject;
using UnityEngine;
using Unity.VisualScripting;
using DG.Tweening;

public class MainCameraView : MonoBehaviour, IAnimView
{

	private void Awake()
    {
		transform.position = new Vector3(-16,15,-16);
		transform.eulerAngles = new Vector3(30,45,0);
    }

}

public class MainCameraViewService : IService
{
	private IViewFabric _fabric;
	private MainCameraView _MainCameraView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _MainCameraView = _fabric.Init<MainCameraView>();
    }

}

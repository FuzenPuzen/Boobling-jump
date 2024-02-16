using Zenject;
using UnityEngine;
using Unity.VisualScripting;
using DG.Tweening;

public class MainCameraView : MonoBehaviour
{
	private ShakeAnim _shakeAnim;

	private void Awake()
    {
		transform.position = new Vector3(-16,15,-16);
		transform.eulerAngles = new Vector3(30,45,0);
        _shakeAnim = transform.GetComponent<ShakeAnim>();
		_shakeAnim.SetValues(0.2f, 1.5f, 40);
    }

	public void Shake()
	{
        _shakeAnim.Play();
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

	public void CameraShake()
	{
        _MainCameraView.Shake();
    }

}

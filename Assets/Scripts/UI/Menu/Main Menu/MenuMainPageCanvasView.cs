using Zenject;
using UnityEngine;

public class MenuMainPageCanvasView : MonoBehaviour
{

}

public class MenuMainPageCanvasViewService : IService
{
	private MenuMainPageCanvasView _menuMainPageView;
	private IViewFabric _fabric;
	private IMarkerService _markerService;


	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
		_menuMainPageView = _fabric.SpawnObject<MenuMainPageCanvasView>();
    }
}

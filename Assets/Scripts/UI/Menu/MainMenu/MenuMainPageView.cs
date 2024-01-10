using Zenject;
using UnityEngine;

public class MenuMainPageView : MonoBehaviour
{

}

public class MenuMainPageViewService : IService
{
	private MenuMainPageView _menuMainPageView;
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
		MonoBehaviour.print(_markerService);
		MonoBehaviour.print(_fabric);
		_menuMainPageView = _fabric.SpawnObject<MenuMainPageView>();
    }
}

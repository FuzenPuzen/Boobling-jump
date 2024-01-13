using Zenject;
using UnityEngine;

public class MenuSkinShopPageCanvasView : MonoBehaviour
{

}

public class MenuSkinShopPageCanvasViewService : IService
{
	private IViewFabric _fabric;
	private MenuSkinShopPageCanvasView _menuSkinShopPageView;
    private IMarkerService _markerService;

	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _menuSkinShopPageView = _fabric.SpawnObject<MenuSkinShopPageCanvasView>();
	}
}
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
		if(_menuSkinShopPageView == null)
        _menuSkinShopPageView = _fabric.Init<MenuSkinShopPageCanvasView>();
	}
}

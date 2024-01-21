using Zenject;
using UnityEngine;

public class MenuSkinShopPageCanvasView : MonoBehaviour
{
    private Canvas _canvas;

    public void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
    }

    internal void HideView()
    {
        gameObject.SetActive(false);
    }

    internal void ShowView()
    {
        gameObject.SetActive(true);
    }
}

public class MenuSkinShopPageCanvasViewService : IService
{
	private IViewFabric _fabric;
	private MenuSkinShopPageCanvasView _menuSkinShopPageView;
    private IMarkerService _markerService;
	private SkinShopItemsService _skinShopItemsService;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService, SkinShopItemsService skinShopItemsService)
	{
        _skinShopItemsService = skinShopItemsService;
        _markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
		if (_menuSkinShopPageView == null)
		{
			_menuSkinShopPageView = _fabric.Init<MenuSkinShopPageCanvasView>();
			_skinShopItemsService.ActivateService();
		}
        _menuSkinShopPageView.ShowView();
    }

    public void HideView()
    {
        _menuSkinShopPageView.HideView();
    }
}

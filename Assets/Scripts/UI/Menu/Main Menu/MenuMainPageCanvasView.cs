using Zenject;
using UnityEngine;
using System;

public class MenuMainPageCanvasView : MonoBehaviour
{

    internal void HideView()
    {
        gameObject.SetActive(false);
    }

    internal void ShowView()
    {
        gameObject.SetActive(true);
    }
}

public class MenuMainPageCanvasViewService : IService
{
	private MenuMainPageCanvasView _menuMainPageView;
	private IViewFabric _fabric;
    private IServiceFabric _serviceFabric;

    private IMarkerService _markerService;

    private IService _menuTutorialPanelViewService;
    private IService _menuSkinShopPanelViewService;
    private IService _menuUpgradePanelViewService;


    [Inject]
	public void Constructor(IViewFabric fabric,
                            IMarkerService markerService,
                            IServiceFabric serviceFabric)
	{
		_markerService = markerService;
		_fabric = fabric;
        _serviceFabric = serviceFabric;
    }

    public void ActivateService()
    {
        if (_menuSkinShopPanelViewService == null)
        {
            _menuSkinShopPanelViewService = _serviceFabric.Init<MenuSkinShopPanelViewService>();
            _menuTutorialPanelViewService = _serviceFabric.Init<MenuTutorialPanelViewService>();
            _menuUpgradePanelViewService = _serviceFabric.Init<MenuUpgradePanelViewService>();

            _menuMainPageView = _fabric.Init<MenuMainPageCanvasView>();
            _menuTutorialPanelViewService.ActivateService();
            _menuUpgradePanelViewService.ActivateService();
            _menuSkinShopPanelViewService.ActivateService();
        }
        _menuMainPageView.ShowView();
    }

	public void HideView()
	{
		_menuMainPageView.HideView();
    }
}

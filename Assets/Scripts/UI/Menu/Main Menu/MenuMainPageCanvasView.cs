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
    private IService _menuInfinityPanelViewService;

    private IService _menuSkinShopPanelViewService;
    private IService _menuUpgradePanelViewService;
    private ISessionTypeDataManager _sessionTypeDataManager;


    [Inject]
	public void Constructor(IViewFabric fabric,
                            IMarkerService markerService,
                            IServiceFabric serviceFabric,
                            ISessionTypeDataManager sessionTypeDataManager)
	{
        _sessionTypeDataManager = sessionTypeDataManager;
        _markerService = markerService;
		_fabric = fabric;
        _serviceFabric = serviceFabric;
    }

    public void ActivateService()
    {
        _menuMainPageView = _fabric.Init<MenuMainPageCanvasView>();
        _menuSkinShopPanelViewService = _serviceFabric.Init<MenuSkinShopPanelViewService>();       
        _menuUpgradePanelViewService = _serviceFabric.Init<MenuUpgradePanelViewService>();
        if (_sessionTypeDataManager.GetTutorialGameType())
        {
            _menuTutorialPanelViewService = _serviceFabric.Init<MenuTutorialPanelViewService>();
            _menuTutorialPanelViewService.ActivateService();
        }
        else
        {
            _menuInfinityPanelViewService = _serviceFabric.Init<MenuInfinityPanelViewService>();
            _menuInfinityPanelViewService.ActivateService();
        }


        _menuUpgradePanelViewService.ActivateService();
        _menuSkinShopPanelViewService.ActivateService();
        HideView();
    }

	public void HideView()
	{
        _menuMainPageView.HideView();
    }

    public void ShowView()
    {
        _menuMainPageView.ShowView();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Zenject;

public class MenuLastState : IBaseState
{
    private IPlayerSkinDataManager _playerSkinDataManager;
    private SkinShopItemsService _skinShopItemsService;
    private IMarkerService _markerService;
    private SettingsPanelViewService _settingsPanelViewService;


    [Inject]
    public void Constructor(IPlayerSkinDataManager playerSkinDataManager, SkinShopItemsService skinShopItemsService,
                            IMarkerService markerService, SettingsPanelViewService settingsPanelViewService)
    {
        _playerSkinDataManager = playerSkinDataManager;
        _skinShopItemsService = skinShopItemsService;
        _markerService = markerService;
        _settingsPanelViewService = settingsPanelViewService;
    }

    public void Enter()
    {
        _settingsPanelViewService.DeactivateService();
        _playerSkinDataManager.DeactivateService();
        _skinShopItemsService.DeactivateService();        
        _markerService.DeActivateService();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}

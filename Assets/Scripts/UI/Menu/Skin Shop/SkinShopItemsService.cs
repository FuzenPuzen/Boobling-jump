using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SkinShopItemsService : IService
{
    private IMarkerService _markerService;
	private IPlayerSkinDataManager _playerSkinDataManager;
	private IServiceFabric _serviceFabric;
	private List<PlayerSkinData> _playerSkinDatas;

	private List<SkinShopItemViewService> _skinShopItemViewServices = new();

    [Inject]
	public void Constructor(IMarkerService markerService,
							IPlayerSkinDataManager playerSkinDataManager,
							IServiceFabric serviceFabric)
	{
        _playerSkinDataManager = playerSkinDataManager;
        _serviceFabric = serviceFabric;
        _markerService = markerService;
	}

	public void ActivateService()
	{

        _playerSkinDatas = _playerSkinDataManager.GetPlayerSkinDatas();
		foreach (PlayerSkinData data in _playerSkinDatas)
		{
            MonoBehaviour.print("12");
            SkinShopItemViewService skinShopItemViewService = _serviceFabric.Init<SkinShopItemViewService>();
			skinShopItemViewService.ActivateService();
            skinShopItemViewService.SetData(data);
            _skinShopItemViewServices.Add(skinShopItemViewService);
        }
    }

	public void UpdateData(List<PlayerSkinData> playerSkinDatas)
	{
        _playerSkinDatas = playerSkinDatas;
        for (int i = 0; i < _playerSkinDatas.Count; i++)
		{
			_skinShopItemViewServices[i].SetData(_playerSkinDatas[i]);
        }
    }
}

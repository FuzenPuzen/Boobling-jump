using EventBus;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SkinShopItemsService : IService
{
    private IMarkerService _markerService;
	private IPlayerSkinDataManager _playerSkinDataManager;
	private IServiceFabric _serviceFabric;
	private List<PlayerSkinData> _playerSkinDatas;
    private EventBinding<OnBuySkin> _onBuySkin;
    private EventBinding<OnChangeSkin> _onChangeSkin;

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
        _onBuySkin = new (UpdateData);
        _onChangeSkin = new (UpdateData);
        _playerSkinDatas = _playerSkinDataManager.GetPlayerSkinDatas();
		foreach (PlayerSkinData data in _playerSkinDatas)
		{
            SkinShopItemViewService skinShopItemViewService = _serviceFabric.InitMultiple<SkinShopItemViewService>();
			skinShopItemViewService.ActivateService();
            skinShopItemViewService.SetData(data);
            _skinShopItemViewServices.Add(skinShopItemViewService);
        }
    }

    public void DeactivateService()
    {
        _onBuySkin.Remove(UpdateData);
        _onChangeSkin.Remove(UpdateData);
    }

	public void UpdateData()
	{
        _playerSkinDatas = _playerSkinDataManager.GetPlayerSkinDatas();
        for (int i = 0; i < _playerSkinDatas.Count; i++)
		{
			_skinShopItemViewServices[i].UpdateData(_playerSkinDatas[i]);
        }
    }
}

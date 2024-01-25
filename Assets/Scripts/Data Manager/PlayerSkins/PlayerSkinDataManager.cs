using EventBus;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IPlayerSkinDataManager : IService
{
    public List<PlayerSkinData> GetPlayerSkinDatas();
    public PlayerSkinData GetCurrentSkin();
}


public class PlayerSkinDataManager : IPlayerSkinDataManager
{
    private PlayerSkinDataCombiner _playerSkinDataCombiner;
    private List<PlayerSkinData> _playerSkinDatas;
    private List<PlayerSkinSLData> _playerSkinSLDatas;
    private ICoinDataManager _coinDataManager;
    private EventBinding<OnTryBuySkin> _onTryBuySkin;

    [Inject]
    public void Constructor(PlayerSkinDataCombiner playerSkinDataCombiner, ICoinDataManager coinDataManager)
    {
        _playerSkinDataCombiner = playerSkinDataCombiner;
        _coinDataManager = coinDataManager;
    }

    public List<PlayerSkinData> GetPlayerSkinDatas() => _playerSkinDatas;

    public void BuySkin(OnTryBuySkin onBuySkin)
    {
        if (_coinDataManager.SpendCoins(onBuySkin.playerSkinData.PlayerSkinSOData.Cost))
        {
            int dataId = onBuySkin.playerSkinData.PlayerSkinSLData.Id;
            _playerSkinSLDatas[dataId].IsOpen = true;
            _playerSkinDatas[dataId].PlayerSkinSLData.IsOpen = true;
            _playerSkinDataCombiner.SaveData(_playerSkinSLDatas);
            EventBus<OnBuySkin>.Raise();
        }
    }

    public void ActivateService()
    {
        _onTryBuySkin = new EventBinding<OnTryBuySkin>(BuySkin);
        _playerSkinDatas = _playerSkinDataCombiner.GetPlayerSkinDatas();
        _playerSkinSLDatas = _playerSkinDataCombiner.GetPlayerSkinSLDatas();
    }

    public PlayerSkinData GetCurrentSkin()
    {
        foreach (PlayerSkinData skinData in _playerSkinDatas)
        {
            if(skinData.PlayerSkinSLData.IsSelected)
                return skinData;
        }
        return _playerSkinDatas[0];
    }
}

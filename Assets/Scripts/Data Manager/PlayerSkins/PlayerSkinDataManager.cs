using EventBus;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IPlayerSkinDataManager : IService
{
    public List<PlayerSkinData> GetPlayerSkinDatas();
}


public class PlayerSkinDataManager : IPlayerSkinDataManager
{
    private PlayerSkinDataCombiner _playerSkinDataCombiner;
    private List<PlayerSkinData> _playerSkinDatas;
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
            onBuySkin.playerSkinData.PlayerSkinSLData.IsOpen = true;
            EventBus<OnBuySkin>.Raise();
        }
    }

    public void ActivateService()
    {
        _onTryBuySkin = new EventBinding<OnTryBuySkin>(BuySkin);
        _playerSkinDatas = _playerSkinDataCombiner.GetPlayerSkinDatas();
    }
}

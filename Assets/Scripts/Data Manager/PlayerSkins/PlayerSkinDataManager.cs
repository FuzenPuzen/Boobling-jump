using EventBus;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IPlayerSkinDataManager : IService
{
    public List<PlayerSkinData> GetPlayerSkinDatas();
    public PlayerSkinData GetCurrentSkin();
    public void DeactivateService();
}


public class PlayerSkinDataManager : IPlayerSkinDataManager
{
    private PlayerSkinDataCombiner _playerSkinDataCombiner;
    private PlayerSkinData _currentSkinData;
    private List<PlayerSkinData> _playerSkinDatas;
    private List<PlayerSkinSLData> _playerSkinSLDatas;
    private ICoinDataManager _coinDataManager;
    private EventBinding<OnTryBuySkin> _onTryBuySkin;
    private EventBinding<OnChangeSkin> _onChangeSkin;

    [Inject]
    public void Constructor(PlayerSkinDataCombiner playerSkinDataCombiner, ICoinDataManager coinDataManager)
    {
        _playerSkinDataCombiner = playerSkinDataCombiner;
        _coinDataManager = coinDataManager;
    }

    public void ChangeSkin(OnChangeSkin onChangeSkin)
    {
        if (onChangeSkin.playerSkinData != null)
        {
            int dataId = onChangeSkin.playerSkinData.PlayerSkinSLData.Id;
            _playerSkinDatas[dataId].PlayerSkinSLData.IsSelected = true;
            int oldSelectedSkinId = _currentSkinData.PlayerSkinSLData.Id;
            _playerSkinDatas[oldSelectedSkinId].PlayerSkinSLData.IsSelected = false;
            _playerSkinSLDatas[oldSelectedSkinId].IsSelected = false;
            _playerSkinDataCombiner.SaveData(_playerSkinSLDatas);
            _currentSkinData = onChangeSkin.playerSkinData;
            EventBus<OnChangeSkin>.Raise();
        }
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
        _onTryBuySkin = new(BuySkin);
        _onChangeSkin = new(ChangeSkin);
        _playerSkinDatas = _playerSkinDataCombiner.GetPlayerSkinDatas();
        _playerSkinSLDatas = _playerSkinDataCombiner.GetPlayerSkinSLDatas();
        _currentSkinData = FindCurrentSkin();
    }

    public void DeactivateService()
    {
        _onTryBuySkin.Remove(BuySkin);
        _onChangeSkin.Remove(ChangeSkin);
    }

    private PlayerSkinData FindCurrentSkin()
    {
        foreach (PlayerSkinData skinData in _playerSkinDatas)
        {
            if (skinData.PlayerSkinSLData.IsSelected)
            {
                _currentSkinData = skinData;
                return _currentSkinData;
            }
        }
        return _playerSkinDatas[0];
    }

    public PlayerSkinData GetCurrentSkin() => _currentSkinData;
}

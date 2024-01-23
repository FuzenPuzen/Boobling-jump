using MLALib;
using ModestTree;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerSkinData
{
    public PlayerSkinData(PlayerSkinSOData PlayerSkinSOData, PlayerSkinSLData PlayerSkinSLData)
    {
        this.PlayerSkinSOData = PlayerSkinSOData;
        this.PlayerSkinSLData = PlayerSkinSLData;
    }

    public PlayerSkinSOData PlayerSkinSOData;
    public PlayerSkinSLData PlayerSkinSLData;
}


public class PlayerSkinDataCombiner 
{
    private const string PlayerSkinDataKey = "PlayerSkinDataKey";
    private ISOStorageService _sOStorageService;

    private PlayerSkinSODatas _playerSkinSODatas;

    private List<PlayerSkinData> _playerSkinDatas = new();

    [Inject]
    public void Constructor(ISOStorageService sOStorageService)
    {
        _sOStorageService = sOStorageService;
        _playerSkinSODatas = (PlayerSkinSODatas)_sOStorageService.GetSOByType<PlayerSkinSODatas>();
        LoadPlayerSkinDatas();
    }

    private void LoadPlayerSkinDatas()
    {
        _playerSkinDatas = SaveLoader.LoadDatas<PlayerSkinData>(_playerSkinDatas, PlayerSkinDataKey);
        //переделать на SL
        if (_playerSkinDatas.IsEmpty())
        {
            for (int i = 0; i < _playerSkinSODatas.dictionary.Count; i++)
            {
                PlayerSkinSLData playerSkinSLData = new();
                _playerSkinDatas.Add(new(_playerSkinSODatas.dictionary[i], playerSkinSLData));
            }
            _playerSkinDatas[0].PlayerSkinSLData.IsOpen = true;
            _playerSkinDatas[0].PlayerSkinSLData.IsSelected = true;
           // SaveLoader.SaveItems<PlayerSkinData>(_playerSkinDatas, PlayerSkinDataKey);
        }
    }

    public List<PlayerSkinData> GetPlayerSkinDatas() => _playerSkinDatas;
}

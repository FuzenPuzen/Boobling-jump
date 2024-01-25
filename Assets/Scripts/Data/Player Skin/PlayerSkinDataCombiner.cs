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
    private List<PlayerSkinSLData> _playerSkinSLDatas = new();

    [Inject]
    public void Constructor(ISOStorageService sOStorageService)
    {
        _sOStorageService = sOStorageService;
        _playerSkinSODatas = (PlayerSkinSODatas)_sOStorageService.GetSOByType<PlayerSkinSODatas>();
        LoadPlayerSkinDatas();
    }

    public List<PlayerSkinData> GetPlayerSkinDatas() => _playerSkinDatas;
    public List<PlayerSkinSLData> GetPlayerSkinSLDatas() => _playerSkinSLDatas;

    public void SaveData(List<PlayerSkinSLData> playerSkinData)
    {
        SaveLoader.SaveItems<PlayerSkinSLData>(playerSkinData, PlayerSkinDataKey);
    }

    private void LoadPlayerSkinDatas()
    {
        _playerSkinSLDatas = SaveLoader.LoadDatas<PlayerSkinSLData>(_playerSkinSLDatas, PlayerSkinDataKey);
        if (_playerSkinSLDatas.IsEmpty())
            CreateSkinDatas();
        else
            FillSkinDatas();
    }

    private void CreateSkinDatas()
    {
        for (int i = 0; i < _playerSkinSODatas.dictionary.Count; i++)
        {
            PlayerSkinSLData playerSkinSLData = new();
            playerSkinSLData.Id = i;
            _playerSkinSLDatas.Add(playerSkinSLData);
            _playerSkinDatas.Add(new(_playerSkinSODatas.dictionary[i], playerSkinSLData));
        }
        _playerSkinDatas[0].PlayerSkinSLData.IsOpen = true;
        _playerSkinDatas[0].PlayerSkinSLData.IsSelected = true;
        SaveLoader.SaveItems<PlayerSkinSLData>(_playerSkinSLDatas, PlayerSkinDataKey);
    }

    private void FillSkinDatas()
    {
        for (int i = 0; i < _playerSkinSODatas.dictionary.Count; i++)
        {
            _playerSkinDatas.Add(new(_playerSkinSODatas.dictionary[i], _playerSkinSLDatas[i]));
        }
    }

}

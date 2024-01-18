using Zenject;
using MLALib;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehaviourDataCombiner
{
    private PlayerSuperJumpBehaviourSODatas _playerSuperJumpBehaviourSODatas;
    public PlayerSuperJumpBehaviourSOData playerSuperJumpBehaviourSOData;
    public PlayerSuperJumpBehaviourSLData playerSuperJumpBehaviourSLData;
    private const string superJumpBehaviourKey = "superJumpBehaviourKey";

    private PlayerRollBehaviourSODatas _playerRollBehaviourSODatas;
    public PlayerRollBehaviourSOData playerRollBehaviourSOData;
    public PlayerRollBehaviourSLData playerRollBehaviourSLData;
    private const string rollBehaviourKey = "rollBehaviourKey";

    private PlayerSimpleBehaviourSODatas _playerSimpleBehaviourSODatas;
    private PlayerSimpleBehaviourSOData _playerSimpleBehaviourSOData;
    private PlayerSimpleBehaviourSLData _playerSimpleBehaviourSLData;
    private const string simpleBehaviourKey = "simpleBehaviourKey";

    private PlayerStartBehaviourSODatas _playerStartBehaviourSODatas;
    private PlayerStartBehaviourSOData _playerStartBehaviourSOData;
    private PlayerStartBehaviourSLData _playerStartBehaviourSLData;
    private const string startBehaviourKey = "startBehaviourKey";

    ISOStorageService _sOStorageService;
    IPlayerBehaviourStorageData _playerBehaviourStorageData;

    [Inject]
    public void Constructor(ISOStorageService sOStorageService, IPlayerBehaviourStorageData playerBehaviourStorageData)
    {
        _sOStorageService = sOStorageService;
        _playerBehaviourStorageData = playerBehaviourStorageData;
        _playerBehaviourStorageData.PlayerBehaviourChanged += SaveData;
        GetAndPullSuperJumpDataToStorage();
        GetAndPullRollDataToStorage();
        GetAndPullSimpleDataToStorage();
        GetAndPullStartDataToStorage();
    }

    private void GetAndPullStartDataToStorage() 
    {
        _playerStartBehaviourSLData = new();
        _playerStartBehaviourSLData = SaveLoader.LoadData<PlayerStartBehaviourSLData>(_playerStartBehaviourSLData, startBehaviourKey);
        _playerStartBehaviourSODatas = GetConvertedSO<PlayerStartBehaviourSODatas>();
        _playerStartBehaviourSOData = _playerStartBehaviourSODatas.dictionary[_playerStartBehaviourSLData.level];
        SetDataToStorage(_playerStartBehaviourSOData, _playerStartBehaviourSLData);
    }

    private void GetAndPullSimpleDataToStorage ()
    {
        _playerSimpleBehaviourSLData = new();
        _playerSimpleBehaviourSLData = SaveLoader.LoadData<PlayerSimpleBehaviourSLData>(_playerSimpleBehaviourSLData, simpleBehaviourKey);
        _playerSimpleBehaviourSODatas = GetConvertedSO<PlayerSimpleBehaviourSODatas>();
        _playerSimpleBehaviourSOData = _playerSimpleBehaviourSODatas.dictionary[_playerSimpleBehaviourSLData.level];
        SetDataToStorage(_playerSimpleBehaviourSOData, _playerSimpleBehaviourSLData);
    }


    private void GetAndPullRollDataToStorage()
    {
        playerRollBehaviourSLData = new();
        playerRollBehaviourSLData = SaveLoader.LoadData<PlayerRollBehaviourSLData>(playerRollBehaviourSLData, rollBehaviourKey);
        _playerRollBehaviourSODatas = GetConvertedSO<PlayerRollBehaviourSODatas>();        
        playerRollBehaviourSOData = _playerRollBehaviourSODatas.dictionary[playerRollBehaviourSLData.level];
        SetDataToStorage(playerRollBehaviourSOData, playerRollBehaviourSLData);
    }

    private void GetAndPullSuperJumpDataToStorage()
    {
        playerSuperJumpBehaviourSLData = new();
        playerSuperJumpBehaviourSLData = SaveLoader.LoadData<PlayerSuperJumpBehaviourSLData>(playerSuperJumpBehaviourSLData, superJumpBehaviourKey);
        _playerSuperJumpBehaviourSODatas = GetConvertedSO<PlayerSuperJumpBehaviourSODatas>();
        playerSuperJumpBehaviourSOData = _playerSuperJumpBehaviourSODatas.dictionary[playerSuperJumpBehaviourSLData.level];
        SetDataToStorage(playerSuperJumpBehaviourSOData, playerSuperJumpBehaviourSLData);
    }

    public void SaveData(ISLData playerBehaviourData)
    {
        switch (playerBehaviourData)
        {
            case PlayerSimpleBehaviourSLData:
                SaveLoader.SaveItem(playerBehaviourData, simpleBehaviourKey);
                break;
            case PlayerRollBehaviourSLData:
                SaveLoader.SaveItem(playerBehaviourData, rollBehaviourKey);
                GetAndPullRollDataToStorage();
                break;
            case PlayerSuperJumpBehaviourSLData:
                SaveLoader.SaveItem(playerBehaviourData, superJumpBehaviourKey);
                GetAndPullSuperJumpDataToStorage();
                break;
        }
    }

    public T GetConvertedSO<T>()
    {
        return _sOStorageService.GetSOByType<T>().ConvertTo<T>();
    }

    public void SetDataToStorage(IPlayerBehaviourData playerBehaviourData, ISLData sLData)
    {
        _playerBehaviourStorageData.SetPlayerBehaviour(playerBehaviourData, sLData);
    }

    public PlayerSuperJumpBehaviourSODatas GetPlayerSuperJumpBehaviourSODatas()
    {
        return _playerSuperJumpBehaviourSODatas;
    }

    public PlayerRollBehaviourSODatas GetPlayerRollBehaviourSODatas()
    {
        return _playerRollBehaviourSODatas;
    }

}

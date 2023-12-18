using Zenject;
using MLALib;
using Unity.VisualScripting;

public class PlayerBehaviourDataCombiner
{
    private PlayerSuperJumpBehaviourSODatas _playerSuperJumpBehaviourSODatas;
    private PlayerSuperJumpBehaviourSOData _playerSuperJumpBehaviourSOData;
    private PlayerSuperJumpBehaviourSLData _playerSuperJumpBehaviourSLData;
    private const string superJumpBehaviourKey = "superJumpBehaviourKey";

    private PlayerRollBehaviourSODatas _playerRollBehaviourSODatas;
    private PlayerRollBehaviourSOData _playerRollBehaviourSOData;
    private PlayerRollBehaviourSLData _playerRollBehaviourSLData;
    private const string rollBehaviourKey = "rollBehaviourKey";

    private PlayerSimpleBehaviourSODatas _playerSimpleBehaviourSODatas;
    private PlayerSimpleBehaviourSOData _playerSimpleBehaviourSOData;
    private PlayerSimpleBehaviourSLData _playerSimpleBehaviourSLData;
    private const string simpleBehaviourKey = "simpleBehaviourKey";

    ISOStorageService _sOStorageService;
    IPlayerBehaviourStorageData _playerBehaviourStorageData;

    [Inject]
    public void Constructor(ISOStorageService sOStorageService, IPlayerBehaviourStorageData playerBehaviourStorageData)
    {
        _sOStorageService = sOStorageService;
        _playerBehaviourStorageData = playerBehaviourStorageData;
        GetAndPullSuperJumpDataToStorage();
        GetAndPullRollDataToStorage();
        GetAndPullSimpleDataToStorage();
    }

    private void GetAndPullSimpleDataToStorage() 
    {
        _playerSimpleBehaviourSLData = new();
        _playerSimpleBehaviourSLData = SaveLoader.LoadData<PlayerSimpleBehaviourSLData>(_playerSimpleBehaviourSLData, simpleBehaviourKey);
        _playerSimpleBehaviourSODatas = GetConvertedSO<PlayerSimpleBehaviourSODatas>();
        _playerSimpleBehaviourSOData = _playerSimpleBehaviourSODatas.dictionary[_playerSimpleBehaviourSLData.level];
        SetDataToStorage(_playerSimpleBehaviourSOData);
    }


    private void GetAndPullRollDataToStorage()
    {
        _playerRollBehaviourSLData = new();
        _playerRollBehaviourSLData = SaveLoader.LoadData<PlayerRollBehaviourSLData>(_playerRollBehaviourSLData, rollBehaviourKey);
        _playerRollBehaviourSODatas = GetConvertedSO<PlayerRollBehaviourSODatas>();        
        _playerRollBehaviourSOData = _playerRollBehaviourSODatas.dictionary[_playerRollBehaviourSLData.level];
        SetDataToStorage(_playerRollBehaviourSOData);
    }

    private void GetAndPullSuperJumpDataToStorage()
    {
        _playerSuperJumpBehaviourSLData = new();
        _playerSuperJumpBehaviourSLData = SaveLoader.LoadData<PlayerSuperJumpBehaviourSLData>(_playerSuperJumpBehaviourSLData, superJumpBehaviourKey);
        _playerSuperJumpBehaviourSODatas = GetConvertedSO<PlayerSuperJumpBehaviourSODatas>();
        _playerSuperJumpBehaviourSOData = _playerSuperJumpBehaviourSODatas.dictionary[_playerSuperJumpBehaviourSLData.level];
        SetDataToStorage(_playerSuperJumpBehaviourSOData);
    }

    public T GetConvertedSO<T>()
    {
        return _sOStorageService.GetSOByType<T>().ConvertTo<T>();
    }

    public void SetDataToStorage(IPlayerBehaviourData playerBehaviourData)
    {
        _playerBehaviourStorageData.SetPlayerBehaviour(playerBehaviourData);
    }


}

using Zenject;
using MLALib;
using Unity.VisualScripting;

public class PlayerBehaviourDataCombiner
{
    private PlayerSuperJumpBehaviourSODatas _playerSuperJumpBehaviourSODatas;
    private PlayerSuperJumpBehaviourSOData _playerSuperJumpBehaviourSOData;
    private PlayerSuperJumpBehaviourSLData _playerSuperJumpBehaviourSLData;
    private const string superJumpBehaviourKey = "superJumpBehaviourKey";

    ISOStorageService _sOStorageService;
    IPlayerBehaviourStorageData _playerBehaviourStorageData;

    [Inject]
    public void Constructor(ISOStorageService sOStorageService, IPlayerBehaviourStorageData playerBehaviourStorageData)
    {
        _sOStorageService = sOStorageService;
        _playerBehaviourStorageData = playerBehaviourStorageData;
        _playerSuperJumpBehaviourSLData = new();
        _playerSuperJumpBehaviourSLData = SaveLoader.LoadData<PlayerSuperJumpBehaviourSLData>(_playerSuperJumpBehaviourSLData, superJumpBehaviourKey);
        _playerSuperJumpBehaviourSODatas = GetConvertedSO<PlayerSuperJumpBehaviourSODatas>();
        SetDataToStorage();
    }

    public T GetConvertedSO<T>()
    {
        return _sOStorageService.GetSOByType<T>().ConvertTo<T>();
    }


    public void SetDataToStorage()
    {
        MatchData();
        _playerBehaviourStorageData.SetPlayerBehaviour(_playerSuperJumpBehaviourSOData);
    }

    public void MatchData()
    {
        // забираем SO соответствующий уровню прокачки
        _playerSuperJumpBehaviourSOData = _playerSuperJumpBehaviourSODatas.dictionary[_playerSuperJumpBehaviourSLData.level];
    }
}

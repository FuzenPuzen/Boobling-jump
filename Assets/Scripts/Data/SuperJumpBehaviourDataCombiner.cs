using UnityEngine;
using Zenject;
using MLALib;
using Unity.VisualScripting;
using UnityEngine.Profiling;

public class SuperJumpBehaviourDataCombiner
{
    private PlayerSuperJumpBehaviourSODatas _playerSuperJumpBehaviourSODatas;
    private PlayerSuperJumpBehaviourSOData _playerSuperJumpBehaviourSOData;
    private PlayerSuperJumpBehaviourSLData _playerSuperJumpBehaviourSLData;
    private IPlayerSuperJumpBehaviourData _playerSuperJumpBehaviourData;
    const string superJumpBehaviourKey = "superJumpBehaviourKey";

    [Inject]
    public void Constructor(ISOStorageService sOStorageService, IPlayerSuperJumpBehaviourData playerSuperJumpBehaviourData)
    {
        _playerSuperJumpBehaviourSODatas = sOStorageService.GetSOByType<PlayerSuperJumpBehaviourSODatas>().ConvertTo<PlayerSuperJumpBehaviourSODatas>();
        _playerSuperJumpBehaviourData = playerSuperJumpBehaviourData;
        LoadData();
        SetDataToStorage();
        MonoBehaviour.print(_playerSuperJumpBehaviourSOData);
    }

    public void SetDataToStorage()
    {
        MatchData();
        _playerSuperJumpBehaviourData.SetSuperJumpBehaviourData(_playerSuperJumpBehaviourSOData);
    }

    public void MatchData()
    {
        _playerSuperJumpBehaviourSOData = _playerSuperJumpBehaviourSODatas.dictionary[_playerSuperJumpBehaviourSLData.level];
    }


    #region SLdata
    public void LoadData()
    {
        if (!PlayerPrefs.HasKey(superJumpBehaviourKey))
        {
            SaveSL(CreateSL());
        }
        else
        {
            LoadSL();
        }
    }
 
    private PlayerSuperJumpBehaviourSLData CreateSL()
    {        
        return _playerSuperJumpBehaviourSLData = new PlayerSuperJumpBehaviourSLData();
    }

    private void LoadSL()
    {
        _playerSuperJumpBehaviourSLData = SaveLoader.LoadItem<PlayerSuperJumpBehaviourSLData>(superJumpBehaviourKey);
    }

    private void SaveSL(PlayerSuperJumpBehaviourSLData data)
    {
        _playerSuperJumpBehaviourSLData = data;
        SaveLoader.SaveItem<PlayerSuperJumpBehaviourSLData>(data, superJumpBehaviourKey);
    }
    #endregion
}

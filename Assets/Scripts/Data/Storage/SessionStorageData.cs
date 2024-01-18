using System;
using System.Collections.Generic;

public class SessionStorageData :
    IPlayerBehaviourStorageData,
    ICoinsStorageData,
    ICurrentScoreStorageData,
    IRecordScoreStorageData,
    IGiftScoreStorageData
{
    private ICurrentScoreData _currentScoreData;
    private ICoinData _coinsData;
    private List<IPlayerBehaviourData> _playerBehaviourDatasSO = new();
    private List<ISLData> _playerBehaviourDatasSL = new();
    private IRecordScoreSLData _recordScoreSLData;
    private IGiftScoreData _giftScoreData;

    public event Action<ISLData> PlayerBehaviourChanged;
    public event Action<ICoinData> CoinDataChanged;
    public event Action<IRecordScoreSLData> RecordDataSlChanged;

    public IPlayerBehaviourData GetPlayerBehaviourData(Type type) 
    {
        return _playerBehaviourDatasSO.Find(x => x.GetType() == type);
    }

    public void SetPlayerBehaviour<T1, T2>(T1 playerBehaviourSO, T2 playerBehaviourSL)
        where T1 : IPlayerBehaviourData
        where T2 : ISLData
    {
        var temp = _playerBehaviourDatasSO.Find(x => x.GetType() == playerBehaviourSO.GetType());
        _playerBehaviourDatasSO.Remove(temp);
        var tempsl = _playerBehaviourDatasSL.Find(x => x.GetType() == playerBehaviourSL.GetType());
        _playerBehaviourDatasSL.Remove(tempsl);

        _playerBehaviourDatasSO.Add(playerBehaviourSO);
        _playerBehaviourDatasSL.Add(playerBehaviourSL);
        //PlayerBehaviourChanged?.Invoke(playerBehaviourSL); 
    }

    public void SetCoinsSLData(ICoinData coinsData)
    {
        _coinsData = coinsData;
        CoinDataChanged?.Invoke(coinsData);
    }

    public ICoinData GetCoinsSLData()
    {
        return _coinsData;
    }

    public ICurrentScoreData GetCurrentScoreData()
    {
        _currentScoreData ??= new CurrentScoreData();
        return _currentScoreData;
    }

    public void SetCurrentScoreData(ICurrentScoreData newCurrentScoreData)
    {
        _currentScoreData = newCurrentScoreData;
    }

    public IRecordScoreSLData GetRecordScoreSLData()
    {
        return _recordScoreSLData;
    }

    public void SetRecordScoreSLData(IRecordScoreSLData newRecordScoreData)
    {
        _recordScoreSLData = newRecordScoreData;
        RecordDataSlChanged?.Invoke(newRecordScoreData);
    }

    public IGiftScoreData GetGiftScoreData()
    {
        return _giftScoreData;
    }

    public void SetGiftScoreData(IGiftScoreData giftScoreData)
    {
        _giftScoreData = giftScoreData;
    }
}

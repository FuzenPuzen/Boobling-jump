using System;
using System.Collections.Generic;

public class SessionStorageData : IPlayerBehaviourStorageData, ICoinsStorageData, ICurrentScoreStorageData, IRecordScoreStorageData
{
    private ICurrentScoreData _currentScoreData;
    private ICoinData _coinsData;
    private List<IPlayerBehaviourData> _playerBehaviourDatas = new();
    private IRecordScoreSLData _recordScoreSLData;

    public event Action<IPlayerBehaviourData> PlayerBehaviourChanged;
    public event Action<ICoinData> CoinDataChanged;
    public event Action<IRecordScoreSLData> RecordDataSlChanged;

    public IPlayerBehaviourData GetPlayerBehaviourData(Type type) 
    {
        return _playerBehaviourDatas.Find(x => x.GetType() == type);
    }

    public void SetPlayerBehaviour<T>(T playerBehaviour) where T : IPlayerBehaviourData
    {
        _playerBehaviourDatas.Add(playerBehaviour);
        PlayerBehaviourChanged?.Invoke(playerBehaviour);
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
}

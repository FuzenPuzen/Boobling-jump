using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionStorageData : IPlayerBehaviourStorageData, ICoinsStoradeData, ICurrentScoreStoradeData
{
    private ICurrentScoreData _currentScoreData;
    private ICoinData _coinsData;
    private List<IPlayerBehaviourData> playerBehaviourDatas = new();

    public event Action<IPlayerBehaviourData> PlayerBehaviourChanged;
    public event Action<ICoinData> CoinDataChanged;

    public IPlayerBehaviourData GetPlayerBehaviourData(Type type) 
    {
        return playerBehaviourDatas.Find(x => x.GetType() == type);
    }

    public void SetPlayerBehaviour<T>(T playerBehaviour) where T : IPlayerBehaviourData
    {
        playerBehaviourDatas.Add(playerBehaviour);
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
        if (_currentScoreData == null) _currentScoreData = new CurrentScoreData();
        return _currentScoreData;
    }

    public void SetCurrentScoreData(ICurrentScoreData newCurrentScore)
    {
        _currentScoreData = newCurrentScore;
    }
}

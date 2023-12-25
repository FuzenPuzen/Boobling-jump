using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionStorageData : IPlayerBehaviourStorageData, ICoinsStoradeData
{
    public ICoinData _coinsData;

    public event Action<IPlayerBehaviourData> PlayerBehaviourChanged;
    public event Action<ICoinData> CoinDataChanged;

    private List<IPlayerBehaviourData> playerBehaviourDatas = new();

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
}

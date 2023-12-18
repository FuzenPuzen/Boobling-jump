using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionStorageData : IPlayerBehaviourStorageData
{
    public event Action<IPlayerBehaviourData> PlayerBehaviourChanged;
    private List<IPlayerBehaviourData> playerBehaviourDatas = new();

    public IPlayerBehaviourData GetPlayerBehaviourData(Type type)
    {
        return playerBehaviourDatas.Find(x => x.GetType() == type);
    }

    public void SetPlayerBehaviour<T>(T playerBehaviour) where T : IPlayerBehaviourData
    {
        playerBehaviourDatas.Add(playerBehaviour);
    }
}

using System;

public interface IPlayerBehaviourStorageData
{
    public IPlayerBehaviourData GetPlayerBehaviourData(Type type);
    public void SetPlayerBehaviour<T1, T2>(T1 playerBehaviourSO, T2 playerBehaviourSL) where T1 : IPlayerBehaviourData where T2 : ISLData;
    public event Action<ISLData> PlayerBehaviourChanged;
}

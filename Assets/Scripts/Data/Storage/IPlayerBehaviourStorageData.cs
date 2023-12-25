using System;

public interface IPlayerBehaviourStorageData
{
    public IPlayerBehaviourData GetPlayerBehaviourData(Type type);
    public void SetPlayerBehaviour<T>(T playerBehaviour) where T : IPlayerBehaviourData;
    public event Action<IPlayerBehaviourData> PlayerBehaviourChanged;
}

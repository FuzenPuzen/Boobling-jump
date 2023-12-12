public interface IPlayerCurrentBehaviourData 
{
    public IPlayerBehaviourData GetPlayerCurrentBehaviourData<T>() where T : IPlayerBehaviourData;
}

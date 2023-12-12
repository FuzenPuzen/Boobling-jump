using System;

public interface IPlayerBehaviorService : Iservice
{
    public void SetBehavior<T1, T2>() where T1 : IPlayerBehavior where T2 : IPlayerBehaviourData;
    public void SetBehaviorEndAction(Action action);
}

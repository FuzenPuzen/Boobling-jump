using System;

public interface IPlayerBehaviorService : Iservice
{
    public void SetBehavior<T>() where T : IPlayerBehavior;
    public void SetBehaviorEndAction(Action action);
}

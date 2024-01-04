using System;

public interface IPlayerBehaviorService : IService
{
    public void SetBehavior<T>() where T : IPlayerBehavior;
    public void SetActionEndBehavior(Action action);
}

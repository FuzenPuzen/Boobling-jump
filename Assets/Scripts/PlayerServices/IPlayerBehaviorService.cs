using UnityEngine;

public interface IPlayerBehaviorService : Iservice
{
    public void SetBehavior<T>() where T : IPlayerBehavior;

}

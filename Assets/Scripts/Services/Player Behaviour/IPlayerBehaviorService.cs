using System;

public interface IPlayerBehaviourService : IService
{
    public void SetBehaviour<T>() where T : IPlayerBehaviour;
    public void SetActionEndBehaviour(Action action);
}

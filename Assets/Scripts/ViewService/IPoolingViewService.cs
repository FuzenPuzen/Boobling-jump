using System;

public interface IPoolingViewService : IService
{
    public void ActivateServiceFromPool();
    public void SetDeactivateAction(Action<IPoolingViewService> action);
}

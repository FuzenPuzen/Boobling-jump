using System;
using Zenject;

public class TimerService : ITimerService
{
    private TimerView _timerView;

    private IFabric _fabric;

    public void ActivateService()
    {
        _timerView = _fabric.SpawnObjectAndGetType<TimerView>();
    }

    [Inject]
    public void Constructor(IFabric fabric)
    {
        _fabric = fabric;
    }

    public void SetActionOnTimerComplete(float delay, Action action)
    {
        _timerView.SetActionOnTimerComplete(delay, action);
    }
}

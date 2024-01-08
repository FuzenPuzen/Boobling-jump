using System;
using Zenject;

public class TimerService : ITimerService
{
    private TimerView _timerView;

    private IViewFabric _fabric;

    public void ActivateService()
    {
        _timerView = _fabric.SpawnObject<TimerView>();
    }

    [Inject]
    public void Constructor(IViewFabric fabric)
    {
        _fabric = fabric;
    }

    public void SetActionOnTimerComplete(float delay, Action action)
    {
        _timerView.SetActionOnTimerComplete(delay, action);
    }
}

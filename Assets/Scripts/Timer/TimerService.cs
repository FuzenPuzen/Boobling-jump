using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public void SetActionOnView(float delay, Action action)
    {
        _timerView.SetAction(delay, action);
    }

    public void SetRepeatActionOnView(float delay, Action action)
    {
        _timerView.SetRepeatAction(delay, action);
    }
}

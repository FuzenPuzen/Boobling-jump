using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TimerService : ITimerService
{
    private TimerView _timerView;

    private PrefabsStorageService _prefabsStorageService;

    [Inject]
    public TimerService(PrefabsStorageService prefabsStorageService)
    {
        _prefabsStorageService = prefabsStorageService;
        _timerView = MonoBehaviour.Instantiate(_prefabsStorageService.GetPrefabByType<TimerView>());
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

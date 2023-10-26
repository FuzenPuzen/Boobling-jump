using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TimerService
{
    private TimerView _timerView;

    public event Action secondAction;

    private PrefabsStorageService _prefabsStorageService;

    [Inject]
    public TimerService(PrefabsStorageService prefabsStorageService)
    {
        _prefabsStorageService = prefabsStorageService;
        _timerView = MonoBehaviour.Instantiate(_prefabsStorageService.GetPrefabByType<TimerView>());
        SetActionOnView();
    }

    private void SetActionOnView()
    {
        _timerView.SetSecondAction(SecondTimerEvent);
    }

    public void SecondTimerEvent()
    {
        secondAction?.Invoke();
    }

}

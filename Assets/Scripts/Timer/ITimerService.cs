using System;
using UnityEngine;

public interface ITimerService : Iservice
{
    public void SetActionOnView(float delay, Action action);
    public void SetRepeatActionOnView(float delay, Action action, ref Coroutine coroutine);
    public void StopRepeatActionOnView(Coroutine coroutine);
}
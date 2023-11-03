using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    public Action secondTimer;

    internal void SetAction(float delay, Action action)
    {
        StartCoroutine(Timer(delay,action));
    }

    internal void SetRepeatAction(float delay, Action action)
    {
        StartCoroutine(RepeatTimer(delay, action));
    }

    private IEnumerator Timer(float delay, Action action)
    {
        yield return new WaitForSecondsRealtime(delay);
        action?.Invoke();
    }

    private IEnumerator RepeatTimer(float delay, Action action)
    {
        yield return new WaitForSecondsRealtime(delay);
        action?.Invoke();
        SetRepeatAction(delay, action);
    }

}

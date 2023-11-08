using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    
    
    internal void SetAction(float delay, Action action)
    {
        StartCoroutine(Timer(delay,action));
    }

    internal void SetRepeatAction(float delay, Action action, ref Coroutine coroutine)
    {
        coroutine = StartCoroutine(RepeatTimer(delay, action, coroutine));
    }

    internal void StopRepeatAction(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }

    private IEnumerator Timer(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    private IEnumerator RepeatTimer(float delay, Action action, Coroutine coroutine)
    {
        yield return new WaitForSeconds(delay);        
        SetRepeatAction(delay, action, ref coroutine);
        action?.Invoke();
    }
}

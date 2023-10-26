using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    
    public Action secondTimer;

    public void SetSecondAction(Action SecondAction)
    {
        secondTimer = SecondAction;
        StartCoroutine(SecondTimer());
    }

    private IEnumerator SecondTimer()
    {
        yield return new WaitForSeconds(1);
        secondTimer?.Invoke();
        StartCoroutine(SecondTimer());
    }

}

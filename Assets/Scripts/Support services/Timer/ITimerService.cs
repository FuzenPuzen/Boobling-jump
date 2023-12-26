using System;
using UnityEngine;

public interface ITimerService : Iservice
{
    public void SetActionOnTimerComplete(float delay, Action action);
  
}
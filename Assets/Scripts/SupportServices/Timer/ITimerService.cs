using System;
using UnityEngine;

public interface ITimerService : IService
{
    public void SetActionOnTimerComplete(float delay, Action action);
  
}
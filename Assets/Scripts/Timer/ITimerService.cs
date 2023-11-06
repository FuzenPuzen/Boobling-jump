using System;

public interface ITimerService : Iservice
{
    public void SetActionOnView(float delay, Action action);
    public void SetRepeatActionOnView(float delay, Action action);
}
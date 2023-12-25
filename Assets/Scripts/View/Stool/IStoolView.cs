using System;

public interface IStoolView
{
    void ActivateView();
    void DeActivateView();

    void OnComplete();


    public event Action CompleteMoveEvent;
}

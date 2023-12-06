using System;

public interface IStoolService
{
    public void SetActionOnMoveComplete(Action<StoolService> action);
    public void ViewCompleteMove();
}

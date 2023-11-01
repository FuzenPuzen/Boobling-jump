using System;

public interface IStoolService
{
    public void SetViewCompleteInstruction(Action<StoolService> action);
}

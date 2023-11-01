using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoolService : IStoolService
{
    private IStoolView _stoolView;
    public Action<StoolService> ViewCompleteMoveAction;

    public StoolService(IStoolView stoolView)
    {
        _stoolView = stoolView;
        _stoolView.ActivateView();
        _stoolView.CompleteMoveEvent += ViewCompleteMove;
    }

    public void SetViewCompleteInstruction(Action<StoolService> action)
    {
        ViewCompleteMoveAction = action;
    }

    public void ViewCompleteMove()
    {
        ViewCompleteMoveAction?.Invoke(this);
    }

}

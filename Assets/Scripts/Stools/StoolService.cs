using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoolService : IStoolService, Iservice
{
    private IStoolView _stoolView;
    public Action<StoolService> ViewCompleteMoveAction;

    public StoolService(IStoolView stoolView)
    {
        _stoolView = stoolView;        
        _stoolView.CompleteMoveEvent += ViewCompleteMove;
    }


    public void ActivateService()
    {
        _stoolView.ActivateView();
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

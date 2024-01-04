using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoolService : IStoolService, IService
{
    private IStoolView _stoolView;
    public Action<StoolService> MoveCompleteAction;

    public StoolService(IStoolView stoolView)
    {
        _stoolView = stoolView;        
        _stoolView.CompleteMoveEvent += ViewCompleteMove;
    }

    public void ActivateService()
    {
        
        _stoolView.ActivateView();
    }

    public void SetActionOnMoveComplete(Action<StoolService> action)
    {
        MoveCompleteAction = action;
    }

    public void ViewCompleteMove()
    {
        MoveCompleteAction?.Invoke(this);
    }
}

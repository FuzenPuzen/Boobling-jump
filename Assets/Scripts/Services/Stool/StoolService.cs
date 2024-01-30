using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoolService : IStoolService, IService
{
    private IStoolView _stoolView;

    public StoolService(IStoolView stoolView)
    {
        _stoolView = stoolView;        
    }

    public void ActivateService()
    {       
        _stoolView.ActivateView();
    }

}

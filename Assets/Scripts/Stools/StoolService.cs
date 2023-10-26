using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoolService : IStoolService
{
    private IStoolView _stoolView;

    public StoolService(IStoolView stoolView)
    {
        _stoolView = stoolView;
        _stoolView.DestroyStool();
    }

}

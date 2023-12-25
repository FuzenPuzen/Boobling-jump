using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoolPoolService
{
    private StoolPoolView _poolView;

    public StoolPoolService(StoolPoolView poolView)
    {
        _poolView = poolView;
    }

    public GameObject GetSectionFromTier(int tierId)
    {
        return _poolView.GetRandomSectionFromTier(tierId);
    }
}

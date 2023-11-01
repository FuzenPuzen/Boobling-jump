using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiersService
{
    private TiersView _tiersView;

    public TiersService(TiersView tiersView)
    {
        _tiersView = tiersView;
    }

    public GameObject GetSectionFromTier(int tierId)
    {
        return _tiersView.GetRandomSectionFromTier(tierId);
    }
}

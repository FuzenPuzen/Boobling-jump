using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RoomViewManager: IRoomViewManager
{
    private IFabric _fabric;
    private RoomView _roomView;

    [Inject]
    public void Constructor(IFabric fabric)
    {
        _fabric = fabric;
        _roomView = _fabric.SpawnObjectAndGetType<RoomView>();
    }
    public void ActivateManager()
    {
        
    }

    public Transform GetCurrentScorePos()
    {
        return _roomView.GetCurrentScorePos();
    }

    public Transform GetGiftScorePos()
    {
        return _roomView.GetGiftScorePos();
    }

    public Transform GetRecordScorePos()
    {
        return _roomView.GetRecordScorePos();
    }
}

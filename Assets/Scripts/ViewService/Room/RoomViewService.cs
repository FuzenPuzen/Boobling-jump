using UnityEngine;
using Zenject;

public class RoomViewService : IService
{
    private IViewFabric _fabric;
    private RoomView _roomView;

    public void ActivateService()
    {
        _roomView = _fabric.Init<RoomView>();
    }

    [Inject]
    public void Constructor(IViewFabric fabric)
    {
        _fabric = fabric;        
    }


}

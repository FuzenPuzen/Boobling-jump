using ModestTree;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Zenject;

public interface IPoolViewService
{
    public void SpawPool<T>(int objCount = 10) where T : IViewService;
    public IViewService GetItem();
    public void ReturnItem(IViewService item);
}

public class PoolViewService : IPoolViewService
{
    private List<IViewService> _freeItems = new();

    private List<IViewService> _viewServices = new();
	private IServiceFabric _serviceFabric;
	private int _objCount;

    [Inject]
	public void Constructor(IServiceFabric serviceFabric)
	{
        _serviceFabric = serviceFabric;
    }

    public IViewService GetItem()
    {
        if (_freeItems.Count == 1) SpawnAddedItem();
        IViewService Item = _freeItems[0];
        _freeItems.Remove(Item);
        return Item;
    }

    public void ReturnItem(IViewService item)  
    {
        _freeItems.Add(item);
    }

    public void SpawPool<T>(int objCount = 10) where T : IViewService
    {
        _objCount = objCount;
        for (int i = 0; i < _objCount; i++)
        {
            IViewService item = _serviceFabric.Create<T>();
            item.SpawnView();
            _viewServices.Add(item);
            _freeItems.Add(item);
        }
    }

    private void SpawnAddedItem()
    {
        IViewService item = (IViewService)_serviceFabric.Create(_freeItems[0].GetType());
        item.SpawnView();
        _freeItems.Add(item);
    }
}

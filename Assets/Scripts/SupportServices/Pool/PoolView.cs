
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using Zenject;

public class PoolView: MonoBehaviour
{

}

public interface IPoolViewService
{
    public void SpawPool(Type objType, int objCount = 10);
    public IPoolingViewService GetItem();
    public void ReturnItem(IPoolingViewService item);
    public int GetViewServicesCount();
}

public class PoolViewService : IPoolViewService
{
    private List<IPoolingViewService> _freeItems = new();

    private List<IPoolingViewService> _viewServices = new();
	private IServiceFabric _serviceFabric;
    private IViewFabric _viewFabric;
    private PoolView _poolView;
	private int _objCount;
    private Type _objType;

    [Inject]
	public void Constructor(IServiceFabric serviceFabric, IViewFabric viewFabric)
	{
        _serviceFabric = serviceFabric;
        _viewFabric = viewFabric;
    }

    public void ActivateService()
    {
        _poolView = _viewFabric.Init<PoolView>();
        
    }

    public int GetViewServicesCount() => _viewServices.Count;

    public IPoolingViewService GetItem()
    {
        if (_freeItems.Count == 1) SpawnAddedItem();
        IPoolingViewService Item = _freeItems.FirstOrDefault();
        _freeItems.Remove(Item);
        return Item;
    }

    public void ReturnItem(IPoolingViewService item)  
    {
        _freeItems.Add(item);
    }

    public void SpawPool(Type objType, int objCount = 10) 
    {
        _objType = objType;
        _objCount = objCount;
        _poolView.name = $"PoolView ({objType.Name})";
        for (int i = 0; i < _objCount; i++)
        {
            SpawnAddedItem();
        }
    }

    private void SpawnAddedItem() 
    {
        IPoolingViewService item = (IPoolingViewService)_serviceFabric.InitMultiple(_objType);
        item.ActivateServiceFromPool(_poolView.transform);
        item.SetDeactivateAction(ReturnItem);
        _viewServices.Add(item);
        _freeItems.Add(item);
    }
}

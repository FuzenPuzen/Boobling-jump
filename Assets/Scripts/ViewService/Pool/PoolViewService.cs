using ModestTree;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public interface IPoolViewService
{
    public void SpawPool<T>(int objCount = 10) where T : IPoolingViewService;
    public IPoolingViewService GetItem();
    public void ReturnItem(IPoolingViewService item);
}

public class PoolViewService : IPoolViewService
{
    private List<IPoolingViewService> _freeItems = new();

    private List<IPoolingViewService> _viewServices = new();
	private IServiceFabric _serviceFabric;
	private int _objCount;

    [Inject]
	public void Constructor(IServiceFabric serviceFabric)
	{
        _serviceFabric = serviceFabric;
    }

    public IPoolingViewService GetItem()
    {
        if (_freeItems.Count == 1) SpawnAddedItem();
        IPoolingViewService Item = _freeItems[0];
        _freeItems.Remove(Item);
        return Item;
    }

    public void ReturnItem(IPoolingViewService item)  
    {
        _freeItems.Add(item);
    }

    public void SpawPool<T>(int objCount = 10) where T : IPoolingViewService
    {
        _objCount = objCount;
        for (int i = 0; i < _objCount; i++)
        {
            IPoolingViewService item = _serviceFabric.Create<T>();
            item.ActivateServiceFromPool();
            _viewServices.Add(item);
            _freeItems.Add(item);
        }
    }

    private void SpawnAddedItem()
    {
        IPoolingViewService item = (IPoolingViewService)_serviceFabric.Create(_freeItems[0].GetType());
        item.ActivateServiceFromPool();
        _viewServices.Add(item);
        _freeItems.Add(item);
    }
}

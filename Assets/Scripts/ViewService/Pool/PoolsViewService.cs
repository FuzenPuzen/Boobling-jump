using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IPoolsViewService : IService
{
	public IPoolViewService GetPool<T>() where T : IPoolingViewService;
}

public class PoolsViewService : IPoolsViewService
{
	private IServiceFabric _serviceFabric;
	private Dictionary<Type, IPoolViewService> _pools = new();

    [Inject]
	public void Constructor(IServiceFabric serviceFabric)
	{
        _serviceFabric = serviceFabric;
    }
	
	public void ActivateService()
	{
		InitPools();
    }

	private void InitPools()
	{

        //_bonusPoolViewService = _serviceFabric.Create<PoolViewService>();

		InitPool<DropedCoinViewService>(10);
		InitPool<DropedRollBonusViewService>(10);
		InitPool<DropedSuperJumpBonusViewService>(10);
    }

	private void InitPool<T>(int count = 0) 
		where T : IPoolingViewService
	{
        PoolViewService newPool = _serviceFabric.Init<PoolViewService>();
         newPool.SpawPool(typeof(T), 10);
		_pools.Add(typeof(T), newPool);
    }
	// ������� �������� ���� � 1 �����

	public IPoolViewService GetPool<T>() where T : IPoolingViewService
	{
        return _pools[typeof(T)];
	}
}

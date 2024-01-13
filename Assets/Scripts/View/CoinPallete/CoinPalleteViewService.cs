﻿using Zenject;
using UnityEngine;

public class CoinPalleteViewService : IService
{
	private CoinPalleteView _coinPalleteView;
	private IViewFabric _fabric;
	private IMarkerService _markerService;
	private ICoinDataManager _coinDataManager;

	private int _coinCountTotal;
	private int _coinCountAdded;
    [Inject]
	public void Constructor(
		IViewFabric fabric,
		IMarkerService markerService,
		ICoinDataManager coinDataManager)
	{
		_fabric = fabric;
		_markerService = markerService;
		_coinDataManager = coinDataManager;
	}

	public void ActivateService()
	{
		Transform target = _markerService.GetTransformMarker<CoinPalleteSpawnPosMarker>().transform;
        _coinPalleteView = _fabric.SpawnObject<CoinPalleteView>(target);
		_coinCountTotal = _coinDataManager.GetCoins();
        _coinPalleteView.UpdateView(_coinCountTotal, _coinCountAdded);
		_coinPalleteView.SetActionCoinCollecte(OnCoinCollected);

    }

	private void OnCoinCollected()
	{
		_coinDataManager.CollectCoins();
	}
}
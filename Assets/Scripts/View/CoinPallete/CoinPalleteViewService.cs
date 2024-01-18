using UnityEngine;
using Zenject;

public class CoinPalleteViewService : IService
{
	private CoinPalleteView _coinPalleteView;
	private IViewFabric _fabric;
	private IMarkerService _markerService;
	private ICoinDataManager _coinDataManager;

	private int _coinCountTotal;
	private int _coinCountCollected;
    private int _cleanCoinCount = 10;

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
        _coinPalleteView = _fabric.Init<CoinPalleteView>(target);
		_coinCountTotal = _coinDataManager.GetCoins();
		_coinDataManager.coinsChanged += _coinPalleteView.UpdateView;
        _coinPalleteView.UpdateView(_coinCountTotal);
		_coinPalleteView.SetActionCoinCollecte(OnCoinCollected);

    }

	private void OnCoinCollected()
	{
		_coinDataManager.CollectCoins();
		_coinCountCollected++;
		if(_coinCountCollected >= _cleanCoinCount)
		{
			_coinCountCollected = 0;
			_coinPalleteView.OpenDoors();
		}
	}
}

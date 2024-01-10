using Zenject;
using UnityEngine;
using System;

public class CoinPalleteView : MonoBehaviour
{
	private Action _cointCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DropedCoinView>(out DropedCoinView component))
        {
			_cointCollected?.Invoke();
        }
    }

    public void SetActionCoinCollecte(Action cointCollected)
    {
        _cointCollected = cointCollected;
    }
}

public class CoinPalleteViewService : IService
{
	private CoinPalleteView _coinPalleteView;
	private IViewFabric _fabric;
	private IMarkerService _markerService;
	private ICoinDataManager _coinDataManager;
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
		_coinPalleteView.SetActionCoinCollecte(OnCoinCollected);

    }

	private void OnCoinCollected()
	{
		_coinDataManager.CollectCoins();
	}
}

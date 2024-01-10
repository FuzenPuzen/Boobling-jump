using Zenject;
using UnityEngine;

public class CoinPalleteView : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GiftCoinView>(out GiftCoinView component))
        {
			// Добавить монетки
			print("Пришла бонусная монетка");
        }
    }
}

public class CoinPalleteViewService : IService
{
	private CoinPalleteView _coinPalleteView;
	private IViewFabric _fabric;
	private IMarkerService _markerService;
	[Inject]
	public void Constructor(
		IViewFabric fabric,
		IMarkerService markerService)
	{
		_fabric = fabric;
		_markerService = markerService;
	}

	public void ActivateService()
	{
		Transform target = _markerService.GetTransformMarker<CoinPalleteSpawnPosMarker>().transform;
        _coinPalleteView = _fabric.SpawnObject<CoinPalleteView>(target);

    }
}

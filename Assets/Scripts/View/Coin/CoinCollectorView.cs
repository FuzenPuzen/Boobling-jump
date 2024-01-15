using Zenject;
using UnityEngine;
using System;

public class CoinCollectorView : MonoBehaviour
{
    private Action _collectAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CoinView>(out CoinView component))
        {
            _collectAction?.Invoke();
        }
    }

    public void SetCollectAction(Action collectAction)
    {
        _collectAction = collectAction;
    }
}

public class CoinCollectorViewService : IService
{
    private CoinCollectorView _coinCollectView;
    private IViewFabric _viewFabric;
    private IMarkerService _markerService;
    private IPoolsViewService _poolsViewService;
    private IPoolViewService _coinPoolViewService;

    [Inject]
    public void Constructor(IViewFabric viewFabric,
                            IMarkerService markerService,
                            IPoolsViewService poolsViewService)
    {
        _poolsViewService = poolsViewService;
        _viewFabric = viewFabric;
        _markerService = markerService;
    }

    public void ActivateService()
	{
        _coinPoolViewService = _poolsViewService.GetPool<DropedCoinViewService>();
        _coinCollectView = _viewFabric.SpawnObject<CoinCollectorView>(_markerService.GetTransformMarker<PlayerMarker>().transform);
        _coinCollectView.SetCollectAction(DropeCoinBonus);
    }

    private void DropeCoinBonus()
    {
        _coinPoolViewService.GetItem().ActivateService();
    }
}

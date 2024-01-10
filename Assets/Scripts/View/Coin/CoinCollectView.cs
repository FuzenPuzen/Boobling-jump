using Zenject;
using UnityEngine;
using System;

public class CoinCollectView : MonoBehaviour
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

public class CoinCollectViewService : IService
{
    private CoinCollectView _coinCollectView;
    private IViewFabric _viewFabric;
    private IMarkerService _markerService;
    private IServiceFabric _serviceFabric;
    [Inject]
    public void Constructor(IViewFabric viewFabric, IMarkerService markerService, IServiceFabric serviceFabric)
    {
        _viewFabric = viewFabric;
        _markerService = markerService;
        _serviceFabric = serviceFabric;
    }

    public void ActivateService()
	{
        _coinCollectView = _viewFabric.SpawnObject<CoinCollectView>(_markerService.GetTransformMarker<PlayerMarker>().transform);
        _coinCollectView.SetCollectAction(DropeCoinBonus);
    }

    private void DropeCoinBonus()
    {
        _serviceFabric.Create<DropedCoinViewService>().ActivateService(_coinCollectView.transform);
    }
}

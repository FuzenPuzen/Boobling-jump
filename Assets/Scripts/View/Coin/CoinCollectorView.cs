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
    private IAudioService _audioService;

    [Inject]
    public void Constructor(IViewFabric viewFabric,
                            IMarkerService markerService,
                            IPoolsViewService poolsViewService,
                            IAudioService audioService)
    {
        _poolsViewService = poolsViewService;
        _viewFabric = viewFabric;
        _markerService = markerService;
        _audioService = audioService;
    }

    public void ActivateService()
	{
        _coinPoolViewService = _poolsViewService.GetPool<DropedCoinViewService>();
        _coinCollectView = _viewFabric.Init<CoinCollectorView>(_markerService.GetTransformMarker<PlayerMarker>().transform);
        _coinCollectView.SetCollectAction(DropeCoinBonus);
    }

    private void DropeCoinBonus()
    {
        _audioService.PlayAudio(AudioEnum.Cash, false);
        _coinPoolViewService.GetItem().ActivateService();
    }
}

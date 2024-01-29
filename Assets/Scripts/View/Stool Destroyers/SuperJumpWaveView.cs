using Zenject;
using UnityEngine;
using System;
using DG.Tweening;

public class SuperJumpWaveView : MonoBehaviour
{
    public Action<Collider> StoolCollideAction;
    public Action DeactivateToPool;
    private Sequence _moveSequence;

    private void OnTriggerEnter(Collider other)
    {
        StoolCollideAction.Invoke(other);
    }

    public void StartWave()
    {
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOScale(new Vector3(6, 6, 6), 2f));
        _moveSequence.OnComplete(DeactivateView);
    }

    private void DeactivateView()
    {
        _moveSequence.Kill();
        transform.localScale = Vector3.zero;
        DeactivateToPool?.Invoke();
        HideView();
    }

    internal void HideView()
    {
        gameObject.SetActive(false);
    }

    internal void ShowView()
    {
        gameObject.SetActive(true);
    }
}

public class SuperJumpWaveViewService : IPoolingViewService
{
    private IViewFabric _fabric;
    private SuperJumpWaveView _superJumpWaveView;
    private Action<IPoolingViewService> _onDeactivateAction;
    private IMarkerService _markerService;
    private IPoolsViewService _poolsViewService;
    private IPoolViewService _coinPoolViewService;

    [Inject]
    public void Constructor(IViewFabric fabric, IMarkerService markerService,
                             IPoolsViewService poolsViewService)
    {
        _poolsViewService = poolsViewService;
        _markerService = markerService;
        _fabric = fabric;
    }

    public void ActivateService()
    {
        StartWave();
    }

    public void OnStoolCollide(Collider other)
    {
        if (other.GetComponent<BasicStoolView>())
        {
            other.GetComponent<BasicStoolView>().DeActivateView();
            _coinPoolViewService.GetItem().ActivateService();
        }
    }

    public void StartWave()
    {
        _superJumpWaveView.ShowView();
        _superJumpWaveView.StartWave();
    }

    public void HideView()
    {
        _superJumpWaveView.HideView();
    }

    public void ShowView()
    {
        _superJumpWaveView.ShowView();
    }

    public void ActivateServiceFromPool()
    {
        _coinPoolViewService = _poolsViewService.GetPool<DropedCoinViewService>();
        Transform parent = _markerService.GetTransformMarker<SuperJumpSpawnPos>().transform;
        _superJumpWaveView = _fabric.Init<SuperJumpWaveView>(parent);
        _superJumpWaveView.StoolCollideAction = OnStoolCollide;
        _superJumpWaveView.DeactivateToPool = DeactivateServiceToPool;
        HideView();
    }

    public void DeactivateServiceToPool()
    {
        _onDeactivateAction?.Invoke(this);
    }

    public void SetDeactivateAction(Action<IPoolingViewService> action)
    {
        _onDeactivateAction = action;
    }
}

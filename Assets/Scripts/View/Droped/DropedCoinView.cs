using Zenject;
using UnityEngine;
using DG.Tweening;
using System;

public class DropedCoinView : MonoBehaviour
{
    public Action _deactivateToPool;

    private Rigidbody _rb;
    private BoxCollider _boxCollider;
    private Sequence _moveSequence;
    private float _duration = 0.25f;
    private Transform _startParent;

    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _startParent = transform.parent;
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);

    }

    public void ActivateView(Transform target)
    {
        gameObject.SetActive(true);
        transform.position = transform.parent.position;
        transform.parent = target;
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOScale(Vector3.one, _duration));
        _moveSequence.Join(transform.DOMove(target.position, _duration));
        _moveSequence.OnComplete(OnComplete);
    }

    public void OnComplete()
    {
        _rb.useGravity = true;
        _rb.isKinematic = false;
        _boxCollider.isTrigger = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<DropedCoinDeactivatorView>())
        {
            DeactivateView();
        }
    }

    private void DeactivateView()
    {
        _moveSequence.Kill();
        _rb.useGravity = false;
        _rb.isKinematic = true;
        _boxCollider.isTrigger = true;
        transform.parent = _startParent;
        transform.localScale = Vector3.zero;
        _deactivateToPool?.Invoke();
        gameObject.SetActive(false);
        
    }
}

public class DropedCoinViewService : IPoolingViewService
{
	private DropedCoinView _dropedCoinView;
	private IViewFabric _fabric;
	private IMarkerService _markerService;
    private Action<IPoolingViewService> _onDeactivateAction;

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
        _dropedCoinView.gameObject.SetActive(true);
        _dropedCoinView.ActivateView(_markerService.GetTransformMarker<CoinPalleteMarker>().transform);
    }

    public void ActivateServiceFromPool()
    {
        if(_dropedCoinView == null)
        {
            Transform parent = _markerService.GetTransformMarker<PlayerMarker>().transform;
            Vector3 spawnPos = parent.position;
            //_dropedCoinView = _dropedCoinView == null? _fabric.SpawnObject<DropedCoinView>(spawnPos,parent) : _dropedCoinView;
            _dropedCoinView = _fabric.Init<DropedCoinView>(spawnPos, parent);
            _dropedCoinView._deactivateToPool = DeactivateServiceToPool;
        }     
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

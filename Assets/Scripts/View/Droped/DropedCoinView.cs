using Zenject;
using UnityEngine;
using DG.Tweening;

public class DropedCoinView : MonoBehaviour
{
    private Rigidbody _rb;
    private BoxCollider _boxCollider;
    private Sequence _moveSequence;
    private float _duration = 0.25f;

    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }


    public void ActivateView(Transform target)
    {
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
}

public class DropedCoinViewService : IPoolingViewService
{
	private DropedCoinView _dropedCoinView;
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
        _dropedCoinView.ActivateView(_markerService.GetTransformMarker<CoinPalleteMarker>().transform);
    }

    public void ActivateServiceFromPool()
    {
        Vector3 spawnPos = _markerService.GetTransformMarker<PlayerMarker>().transform.position;
        Transform parent = _markerService.GetTransformMarker<PlayerMarker>().transform;
        _dropedCoinView = _fabric.SpawnObject<DropedCoinView>(spawnPos,parent);
    }

}

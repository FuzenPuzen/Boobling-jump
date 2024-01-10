using Zenject;
using UnityEngine;
using DG.Tweening;

public class DropedCoinView : MonoBehaviour
{
    private Rigidbody _rb;
    private BoxCollider _boxCollider;
    private Sequence _moveSequence;
    private Transform _target;
    private Vector3 _velocity;
    private float _duration = 0.25f;
    private bool _isTargetAchived;

    public void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void ActivateView(Transform target)
    {
        _target = target;
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

public class DropedCoinViewService
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

	public void ActivateService(Transform target)
	{
		_dropedCoinView = _fabric.SpawnObject<DropedCoinView>(target.position);
		_dropedCoinView.ActivateView(_markerService.GetTransformMarker<CoinPalleteMarker>().transform);
	}
}

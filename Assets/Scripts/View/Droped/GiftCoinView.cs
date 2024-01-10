using Zenject;
using UnityEngine;
using System;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class GiftCoinView : MonoBehaviour
{
    private Sequence _moveSequence;
    private Transform _target;
    private Vector3 _velocity;
    private float _duration = 0.5f;

    public void ActivateView(Transform target)
    {
        _target = target;
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOScale(Vector3.one, 0.25f));
    }

    public void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _target.position, ref _velocity, _duration);
    }
}

public class GiftCoinViewService
{
	private GiftCoinView _dropedCoinView;
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
		_dropedCoinView = _fabric.SpawnObject<GiftCoinView>(target.position);
		_dropedCoinView.ActivateView(_markerService.GetTransformMarker<CoinPalleteMarker>().transform);
	}
}

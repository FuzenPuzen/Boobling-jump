using Zenject;
using UnityEngine;
using System;
using DG.Tweening;

public class DropedSuperJumpBonusView : MonoBehaviour
{
    public Action _deactivateToPool;
    private Sequence _moveSequence;
    private float _duration = 0.25f;
    private Transform _startParent;

    public void Awake()
    {
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
        _moveSequence.OnComplete(DeactivateView);
    }

    private void DeactivateView()
    {
        _moveSequence.Kill();
        transform.parent = _startParent;
        transform.localScale = Vector3.zero;
        _deactivateToPool?.Invoke();
        gameObject.SetActive(false);
    }
}

public class DropedSuperJumpBonusViewService : IPoolingViewService
{
	private IViewFabric _fabric;
	private DropedSuperJumpBonusView _dropedSuperJumpBonusView;
    private IMarkerService _markerService;
    private Action<IPoolingViewService> _onDeactivateAction;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
        _dropedSuperJumpBonusView.ActivateView(_markerService.GetTransformMarker<SuperJumpBonusBlenderPosMarker>().transform);
    }

    public void ActivateServiceFromPool()
    {
        if (_dropedSuperJumpBonusView == null)
        {
            Transform parent = _markerService.GetTransformMarker<PlayerMarker>().transform;
            Vector3 spawnPos = parent.position;
            _dropedSuperJumpBonusView = _fabric.SpawnObject<DropedSuperJumpBonusView>(spawnPos, parent);
            _dropedSuperJumpBonusView._deactivateToPool = DeactivateServiceToPool;
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

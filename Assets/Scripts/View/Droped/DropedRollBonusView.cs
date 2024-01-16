using Zenject;
using UnityEngine;
using System;
using DG.Tweening;

public class DropedRollBonusView : MonoBehaviour
{
    public Action _deactivateToPool;
    private Sequence _moveSequence;
    private float _duration = 0.25f;
    private Transform _startParent;

    public void Awake()
    {
        _startParent = transform.parent;
    }

    public void ActivateView(Transform target)
    {
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
        transform.gameObject.SetActive(false);
    }
}

public class DropedRollBonusViewService : IPoolingViewService
{
	private IViewFabric _fabric;
	private DropedRollBonusView _dropedRollBonusView;
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
        _dropedRollBonusView.gameObject.SetActive(true);
        _dropedRollBonusView.ActivateView(_markerService.GetTransformMarker<RollBonusBlenderPosMarker>().transform);
    }

    public void ActivateServiceFromPool()
    {
        if(_dropedRollBonusView == null)
        {
            Transform parent = _markerService.GetTransformMarker<PlayerMarker>().transform;
            Vector3 spawnPos = parent.position;
            _dropedRollBonusView = _fabric.SpawnObject<DropedRollBonusView>(spawnPos, parent);
            _dropedRollBonusView._deactivateToPool = DeactivateServiceToPool;
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

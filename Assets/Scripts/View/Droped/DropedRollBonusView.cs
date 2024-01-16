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
        _moveSequence.OnComplete(()=>_deactivateToPool?.Invoke());
    }

    public void DeactivateView()
    {
        _moveSequence.Kill();
        transform.parent = _startParent;
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
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
        Transform target = _markerService.GetTransformMarker<RollBonusBlenderPosMarker>().transform;
        _dropedRollBonusView.ActivateView(target);
    }

    public void ActivateServiceFromPool()
    {
        if(_dropedRollBonusView == null)
        {
            Transform parent = _markerService.GetTransformMarker<PlayerMarker>().transform;
            _dropedRollBonusView = _fabric.SpawnObject<DropedRollBonusView>(parent);
            _dropedRollBonusView._deactivateToPool = DeactivateServiceToPool;
        }
    }
    public void DeactivateServiceToPool()
    {
        _dropedRollBonusView.DeactivateView();
        _onDeactivateAction?.Invoke(this);
    }

    public void SetDeactivateAction(Action<IPoolingViewService> action)
    {
        _onDeactivateAction = action;
    }
}

using Zenject;
using UnityEngine;
using System;
using System.Collections;
using EventBus;
using DG.Tweening;
using UnityEngine.UIElements;

public class SuperJumpBonusBlenderView : MonoBehaviour
{
    public Action collectAction;
    private float _duration = 5f;
    private Action _bonusBlenderEndAction;
    private Sequence _scale;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DropedSuperJumpBonusView>(out DropedSuperJumpBonusView component))
        {
            print("Blender Check Bonus SuperJump");
            collectAction?.Invoke();
        }
    }

    public void BlenderStart(Action action)
    {
        _scale.Kill();
        _scale = DOTween.Sequence();
        _scale.Append(transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.25f));
        _bonusBlenderEndAction = action;

        StartCoroutine(BonusBlenderDuration());
    }

    private IEnumerator BonusBlenderDuration()
    {
        yield return new WaitForSeconds(_duration);
        BlenderEnd();
    }

    public void BlenderEnd()
    {
        _scale.Kill();
        _scale = DOTween.Sequence();
        _scale.Append(transform.DOScale(Vector3.one, 0.25f));
        _bonusBlenderEndAction?.Invoke();
    }
}

public class SuperJumpBonusBlenderViewService : IService
{
	private IViewFabric _fabric;
	private SuperJumpBonusBlenderView _superJumpBonusBlenderView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _superJumpBonusBlenderView = _fabric.SpawnObject<SuperJumpBonusBlenderView>(_markerService.GetTransformMarker<SuperJumpBonusBlenderPosMarker>().transform);
        _superJumpBonusBlenderView.collectAction = OnSuperJumpActivate;
    }

    public void DeactivateService()
    {
        _superJumpBonusBlenderView.collectAction = null;
    }

    private void OnSuperJumpActivate()
    {
        EventBus<OnSupperJumpActivate>.Raise();
        _superJumpBonusBlenderView.BlenderStart(OnSuperJumpDeactivate);
    }

    private void OnSuperJumpDeactivate()
    {
        EventBus<OnSupperJumpDeactivate>.Raise();
    }
}

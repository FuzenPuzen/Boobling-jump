using Zenject;
using UnityEngine;
using System;
using EventBus;
using System.Collections;
using DG.Tweening;
using UnityEngine.UIElements;

public class RollBonusBlenderView : MonoBehaviour
{
    public Action collectAction;
	private float _duration;
	private Action _bonusBlenderEndAction;

	private Sequence _scale;

    public void SetDuration(float duration)
    {
        _duration = duration;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DropedRollBonusView>(out DropedRollBonusView component))
        {
            print("Blender Check Bonus Roll");
            collectAction?.Invoke();
        }
    }

	public void BlenderStart(Action action)
	{
        _scale.Kill();
        _scale = DOTween.Sequence();
        _bonusBlenderEndAction = action;
		_scale.Append(transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.25f));
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

public class RollBonusBlenderViewService : IService
{
	private IViewFabric _fabric;
	private RollBonusBlenderView _rollBonusBlenderView;
    private IMarkerService _markerService;
    private IPlayerBehaviourDataManager _playerBehaviourDataManager;
    private IAudioService _audioService;
    [Inject]
	public void Constructor(
        IViewFabric fabric,
        IMarkerService markerService,
        IPlayerBehaviourDataManager playerBehaviourDataManager,
        IAudioService audioService)
	{
        _playerBehaviourDataManager = playerBehaviourDataManager;
        _markerService = markerService;
		_fabric = fabric;
        _audioService = audioService;
	}

	public void ActivateService()
	{       
        _rollBonusBlenderView = _fabric.Init<RollBonusBlenderView>(_markerService.GetTransformMarker<RollBonusBlenderPosMarker>().transform);
		_rollBonusBlenderView.collectAction = OnRollActivate;
        _rollBonusBlenderView.SetDuration(_playerBehaviourDataManager.GetRollCurrentDuration());
    }

    public void DeactivateService()
    {
        _rollBonusBlenderView.collectAction = null;
    }
    private void OnRollActivate() 
	{
        _audioService.PlayAudio(AudioEnum.SuperRotate, false);
		EventBus<OnRollActivate>.Raise();
		_rollBonusBlenderView.BlenderStart(OnRollDeactivate);
	}

	private void OnRollDeactivate()
	{
        EventBus<OnRollDeactivate>.Raise();
    }
}

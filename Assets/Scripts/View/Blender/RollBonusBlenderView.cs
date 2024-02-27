using Zenject;
using UnityEngine;
using System;
using EventBus;
using System.Collections;
using DG.Tweening;
using UnityEngine.UIElements;
using TMPro;

public class RollBonusBlenderView : BonusBlenderView
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DropedRollBonusView>(out DropedRollBonusView component))
        {
            collectAction?.Invoke();
        }
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

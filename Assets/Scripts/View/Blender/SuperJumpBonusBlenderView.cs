using Zenject;
using UnityEngine;
using System;
using System.Collections;
using EventBus;
using DG.Tweening;
using UnityEngine.UIElements;
using TMPro;

public class SuperJumpBonusBlenderView : BonusBlenderView
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DropedSuperJumpBonusView>(out DropedSuperJumpBonusView component))
        {
            collectAction?.Invoke();
        }
    }
}

public class SuperJumpBonusBlenderViewService : IService
{
	private IViewFabric _fabric;
	private SuperJumpBonusBlenderView _superJumpBonusBlenderView;
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
        _superJumpBonusBlenderView = _fabric.Init<SuperJumpBonusBlenderView>(_markerService.GetTransformMarker<SuperJumpBonusBlenderPosMarker>().transform);
        _superJumpBonusBlenderView.collectAction = OnSuperJumpActivate;
        _superJumpBonusBlenderView.SetDuration(_playerBehaviourDataManager.GetSuperJumpCurrentDuration());
    }

    public void DeactivateService()
    {
        _superJumpBonusBlenderView.collectAction = null;
    }

    private void OnSuperJumpActivate()
    {
        _audioService.PlayAudio(AudioEnum.SuperJump, false);
        EventBus<OnSupperJumpActivate>.Raise();
        _superJumpBonusBlenderView.BlenderStart(OnSuperJumpDeactivate);
    }

    private void OnSuperJumpDeactivate()
    {
        EventBus<OnSupperJumpDeactivate>.Raise();
    }
}

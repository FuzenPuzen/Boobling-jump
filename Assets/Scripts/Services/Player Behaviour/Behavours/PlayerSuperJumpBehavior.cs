using DG.Tweening;
using System;
using UnityEngine;
using EventBus;
using Zenject;
using Random = UnityEngine.Random;

public class PlayerSuperJumpBehaviour : PlayerJumpBehaviour
{
    private PlayerSuperJumpBehaviourSOData _PlayerBehaviourData;
    private IAnimationService _animationService;
    private IAudioService _audioService;


    [Inject]
    public void Constructor(IAnimationService animationService, IAudioService audioService)
    {
        _animationService = animationService;
        _audioService = audioService;
    }

    public override void Fall(TweenCallback tweenCallback)
    {
        base.Fall(FallCallback);
    }

    public override void FallCallback()
    {
        ShakeAnimData shakeAnimData = SetAnimValues();
        _animationService.PlayAnimation<ShakeAnim, MainCameraView>(shakeAnimData);
        _audioService.PlayAudio(Random.Range(0, 2) == 0 ? AudioEnum.Bum : AudioEnum.Bam, false);
        EventBus<OnSupperJumpFall>.Raise();
        base.FallCallback();
    }

    private static ShakeAnimData SetAnimValues()
    {
        ShakeAnimData shakeAnimData = new();
        shakeAnimData.Duration = 0.1f;
        shakeAnimData.ShakeVibrato = 100;
        shakeAnimData.ShakeForce = 1.1f;
        return shakeAnimData;
    }

    public override void ColliderBehaviour(Collider other) { }

    public override Type GetBehaviourDataType()
    {
        return typeof(PlayerSuperJumpBehaviourSOData);
    }

    public override void SetBehaviourData(IPlayerBehaviourData playerBehaviourData)
    {
        _PlayerBehaviourData = (PlayerSuperJumpBehaviourSOData)playerBehaviourData;
    }

}

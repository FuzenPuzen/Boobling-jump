using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;

[ShowOdinSerializedPropertiesInInspector]
public class ShakeAnim : Anim
{
    private float _shakeDuration;
    private float _shakeForce;
    private int _shakeVibrato;
    [SerializeField] private ShakeAnimData _currentAnimData;


    public override void OnEnable()
    {
        _animData = (AnimData)_currentAnimData;
        SetValues(_currentAnimData);
        base.OnEnable();
    }

    public override void Play()
    {
        _animSequence.Kill();
        _animSequence = DOTween.Sequence();
        _animSequence.Append(transform.DOShakePosition(_shakeDuration,
                                                       _shakeForce,
                                                       _shakeVibrato,
                                                       fadeOut: false,
                                                       randomnessMode: ShakeRandomnessMode.Harmonic));
    }

    public override void SetValues(AnimData AnimData)
    {
        ShakeAnimData shakeAnimData = AnimData as ShakeAnimData;
        shakeAnimData = shakeAnimData ?? new();
        _shakeDuration = shakeAnimData.Duration;
        _shakeForce = shakeAnimData.ShakeForce;
        _shakeVibrato = shakeAnimData.ShakeVibrato;
    }

}

[Serializable]
public class ShakeAnimData : AnimData
{
   public float ShakeForce = 1.2f;
   public int ShakeVibrato = 10;
}


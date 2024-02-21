using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeAnim : Anim
{
    private float _shakeDuration;
    private float _shakeForce;
    private int _shakeVibrato;

    public override void Play()
    {
        _animSequence.Kill();
        _animSequence = DOTween.Sequence();
        _animSequence.Append(transform.DOShakePosition(_shakeDuration, _shakeForce, _shakeVibrato));
    }

    public override void SetValues(AnimData AnimData)
    {
        ShakeAnimData shakeAnimData = AnimData as ShakeAnimData;
        shakeAnimData = shakeAnimData ?? new();
        _shakeDuration = shakeAnimData.Duration;
        _shakeForce = shakeAnimData.ShakeForce;
        _shakeVibrato = shakeAnimData.ShakeVibrato;
    }

    public override void Stop()
    {
        
    }
}


public class ShakeAnimData : AnimData
{
   public float ShakeForce = 1.2f;
   public int ShakeVibrato = 10;
}
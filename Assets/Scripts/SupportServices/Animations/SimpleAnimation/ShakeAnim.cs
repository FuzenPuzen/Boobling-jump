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
        transform.DOShakePosition(_shakeDuration, _shakeForce, _shakeVibrato);
    }

    public override void SetValues(AnimData AnimData)
    {
        if (AnimData == null) return;
        var shakeAnimData = AnimData as ShakeAnimData;
        _shakeDuration = shakeAnimData.ShakeDuration;
        _shakeForce = shakeAnimData.ShakeForce;
        _shakeVibrato = shakeAnimData.ShakeVibrato;
    }
}


public class ShakeAnimData : AnimData
{
   public float ShakeDuration = 0.2f;
   public float ShakeForce = 1.2f;
   public int ShakeVibrato = 10;
}


public abstract class Anim : MonoBehaviour
{
    public abstract void Play();

    public abstract void SetValues(AnimData shakeAnimData);
}
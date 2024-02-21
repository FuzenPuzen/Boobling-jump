using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

[ShowOdinSerializedPropertiesInInspector]
public class ShakeAnim : Anim
{
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeForce;
    [SerializeField] private int _shakeVibrato;
    [SerializeField] private bool _loop;
    [SerializeField] private bool _autoPlay;
    [ShowIf("_autoPlay")]
    [SerializeField] private float _startDelay;

    public override void Play()
    {
        _animSequence.Kill();
        _animSequence = DOTween.Sequence();
        _animSequence.Append(transform.DOShakePosition(_shakeDuration, _shakeForce, _shakeVibrato));
    }

    public override void SetValues(AnimData AnimData)
    {
        // Ошибка если не передавать AnimData 
        var shakeAnimData = AnimData as ShakeAnimData;
        _shakeDuration = shakeAnimData.Duration;
        _shakeForce = shakeAnimData.ShakeForce;
        _shakeVibrato = shakeAnimData.ShakeVibrato;
    }

    public override void Stop()
    {
        _animSequence.Kill();
    }
}


public class ShakeAnimData : AnimData
{
   public float ShakeForce = 1.2f;
   public int ShakeVibrato = 10;
}


public abstract class Anim : MonoBehaviour
{
    public Sequence _animSequence;
    public abstract void Play();
    public abstract void Stop();

    public abstract void SetValues(AnimData shakeAnimData);

    public void OnDestroy()
    {
        _animSequence.Kill();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeAnim : MonoBehaviour
{
    [SerializeField] private float _shakeDuration = 1;
    [SerializeField] private float _shakeForce = 2;
    [SerializeField] private int _shakeVibrato = 100;

    public void Play()
    {
        transform.DOShakePosition(_shakeDuration, _shakeForce, _shakeVibrato);
    }

    public void SetValues(float shakeDuration, float shakeForce, int shakeVibrato)
    {
        _shakeDuration = shakeDuration;
        _shakeForce = shakeForce;
        _shakeVibrato = shakeVibrato;
    }

}

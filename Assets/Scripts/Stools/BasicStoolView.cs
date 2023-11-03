using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class BasicStoolView : MonoBehaviour, IStoolView
{
    protected float _moveTarget = 18;
    protected float _movingTime = 6f;
    protected DG.Tweening.Sequence _moveSequence;
    public event Action CompleteMoveEvent;


    public virtual void ActivateView()
    {
        StartMove();
    }

    public virtual void StartMove()
    {
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOMoveX(_moveTarget, _movingTime)).OnComplete(OnComplete);
    }

    public void OnComplete()
    {
        CompleteMoveEvent?.Invoke();
        DeActivateView();
    }

    public virtual void DeActivateView()
    {
        _moveSequence.Kill();
        transform.localPosition = Vector3.zero;
    }
}

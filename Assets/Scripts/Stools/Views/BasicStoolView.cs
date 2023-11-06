using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class BasicStoolView : MonoBehaviour, IStoolView
{
    protected float _moveTarget = 11f;
    protected float _movingTime = 2.5f;
    protected DG.Tweening.Sequence _moveSequence;
    public event Action CompleteMoveEvent;


    public virtual void ActivateView()
    {
        transform.localScale = Vector3.zero;
        StartMove();
    }


    public virtual void StartMove()
    {
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOScale(Vector3.one , 0.25f));
        _moveSequence.Append(transform.DOMoveX(_moveTarget, _movingTime).SetEase(Ease.Linear));
        _moveSequence.Append(transform.DOScale(Vector3.zero, 0.25f)).OnComplete(OnComplete);
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

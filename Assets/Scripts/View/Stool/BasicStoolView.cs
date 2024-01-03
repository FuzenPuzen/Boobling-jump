using UnityEngine;
using DG.Tweening;
using System;

public class BasicStoolView : MonoBehaviour, IStoolView
{
    protected float _moveTarget = -7f;
    protected float _movingTime = 2.5f;
    protected Sequence _moveSequence;
    public event Action CompleteMoveEvent;

    public virtual void ActivateView()
    {
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.zero;
        //StartMove();
    }

    public virtual void StartMove()
    {
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOScale(Vector3.one , 0.25f));
        _moveSequence.Append(transform.DOMoveX(_moveTarget, _movingTime).SetEase(Ease.Linear));
        _moveSequence.Insert(_movingTime,transform.DOLocalRotate(new(0, 0, 180), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
        _moveSequence.Join(transform.DOMoveY(_moveTarget, 1f).SetEase(Ease.Linear)).OnComplete(OnComplete);
    }

    public void OnComplete()
    {       
        CompleteMoveEvent?.Invoke();
        DeActivateView();
    }

    public virtual void DeActivateView()
    {
        _moveSequence.Kill();
        CompleteMoveEvent?.Invoke();
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

}

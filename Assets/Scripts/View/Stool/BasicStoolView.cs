using UnityEngine;
using DG.Tweening;
using System;

public class BasicStoolView : MonoBehaviour, IStoolView
{
    protected Sequence _moveSequence;
    protected Sequence _fallSequence;
    protected Vector3 _startPos;
    public event Action CompleteMoveEvent;

    public virtual void ActivateView()
    {
        _startPos = transform.position;
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOScale(Vector3.one, 0.25f));
    }

    public void Fall()
    {
        _fallSequence = DOTween.Sequence();
        _fallSequence.Append(transform.DOLocalRotate(new(0, 0, 180), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
        _fallSequence.Join(transform.DOMoveY(-10, 1f)).OnComplete(OnComplete);
    }

    public void OnComplete()
    {       
        CompleteMoveEvent?.Invoke();
        SetStartValues();
        DeActivateView();
    }

    public virtual void DeActivateView()
    {
        //gameObject.SetActive(false); //Под вопросом
    }

    public virtual void SetStartValues()
    {
        transform.position = _startPos;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.zero;
    }


    private void OnTriggerEnter(Collider other)
    {
        ActivateView();
    }

    private void OnTriggerExit(Collider other)
    {
        Fall();
    }

}

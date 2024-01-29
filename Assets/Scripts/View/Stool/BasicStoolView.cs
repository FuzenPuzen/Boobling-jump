using UnityEngine;
using DG.Tweening;
using System;

public class BasicStoolView : MonoBehaviour, IStoolView
{
    protected Sequence _moveSequence;
    protected Sequence _fallSequence;
    protected Vector3 _startPos;
    public event Action CompleteMoveEvent;

    public void Start()
    {
        _startPos = transform.localPosition;
    }

    public virtual void ActivateView()
    {       
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
        DeActivateView();
    }

    public virtual void DeActivateView()
    {
        _fallSequence.Kill();
        _moveSequence.Kill();
        SetStartValues();
    }

    public virtual void SetStartValues()
    {
        transform.localPosition = _startPos;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.zero;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SectionActivator"))
        {            
            ActivateView();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SectionActivator"))
        {
            Fall();
        }
    }

}

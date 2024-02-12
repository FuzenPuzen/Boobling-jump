using UnityEngine;
using DG.Tweening;
using System;

public class BasicStoolView : MonoBehaviour, IStoolView, IView
{
    protected Sequence _moveSequence;
    protected Sequence _fallSequence;
    protected Vector3 _startPos;
    public event Action CompleteMoveEvent;
    public bool CanSpawnCoin;

    public void Start()
    {
        _startPos = transform.localPosition;
        SetStartValues();
    }

    public virtual void ActivateView()
    {
        CanSpawnCoin = true;
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOScale(Vector3.one, 0.25f));
    }

    public void Fall()
    {
        CanSpawnCoin = false;
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
        CanSpawnCoin = false;
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
        if (other.GetComponent<SectionActivator>())
        {            
            ActivateView();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SectionActivator>())
        {
            Fall();
        }
    }

    public void DeactivateView()
    {
        DeActivateView();
    }

    public bool GetCanSpawnCoin() => CanSpawnCoin;
}

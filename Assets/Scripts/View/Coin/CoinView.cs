using DG.Tweening;
using System;
using UnityEngine;

public class CoinView : MonoBehaviour, IView
{
    private Sequence _moveSequence;
    protected Sequence _fallSequence;
    protected float _moveTarget = -5.5f;
    protected float _movingTime = 2.5f;
    protected Vector3 _startPos;
    public bool CanSpawnCoin;

    public void Start()
    {
        _startPos = transform.localPosition;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CoinCollectorView>())
        {
            OnCollect();

        }
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

    public void Fall()
    {
        CanSpawnCoin = false;
        _fallSequence = DOTween.Sequence();
        _fallSequence.Append(transform.DOLocalRotate(new(0, 0, 180), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
        _fallSequence.Join(transform.DOMoveY(-10, 1f)).OnComplete(OnComplete);
    }

    public void OnComplete()
    {
        DeactivateView();
    }

    public virtual void SetStartValues()
    {
        transform.localPosition = _startPos;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.zero;
    }

    public void OnCollect()
    {
        _moveSequence.Kill();
        DeactivateView();
    }

    public virtual void ActivateView()
    {
        CanSpawnCoin = true;
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOScale(Vector3.one, 0.25f));
    }

    public void DeactivateView()
    {
        CanSpawnCoin = false;
        SetStartValues();
    }

    public bool GetCanSpawnCoin() => CanSpawnCoin;
}

using DG.Tweening;
using System;
using UnityEngine;

public class CoinView : MonoBehaviour
{
    private Action<CoinView> CollectAction;
    private Sequence _moveSequence;
    protected float _moveTarget = 11f;
    protected float _movingTime = 2.5f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collector"))
        {
            CollectAction?.Invoke(this);
        }
    }

    public void SetCollectAction(Action<CoinView> action)
    {
        CollectAction = action;
    }

    public virtual void StartMove()
    {
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOScale(Vector3.one, 0.25f));
        _moveSequence.Append(transform.DOMoveX(_moveTarget, _movingTime).SetEase(Ease.Linear));
        _moveSequence.Append(transform.DOScale(Vector3.zero, 0.25f));
    }

}

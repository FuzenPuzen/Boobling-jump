using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketStool : BasicStoolView
{
    public override void StartMove()
    {
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOMoveX(_moveTarget, _movingTime / 2)).SetEase(Ease.InQuart).OnComplete(OnComplete);
    }
}

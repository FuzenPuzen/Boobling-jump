using UnityEngine;
using DG.Tweening;

public class SectionView : MonoBehaviour
{

    private Sequence _moveSequence;
    private float _endPosX = -7f;
    private float _movingTime = 2.5f;

    public void StartMove()
    {
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOMoveX(_endPosX, _movingTime).SetEase(Ease.Linear));
    }
}

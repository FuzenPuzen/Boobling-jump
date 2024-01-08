using UnityEngine;
using DG.Tweening;
using System;

public class SectionView : MonoBehaviour
{
    private Action _sectionActivatorExitAction;
    private Action _sectionActivatorEnterAction;
    private Sequence _moveSequence;
    private float _endPosX = -42f;
    private float _movingTime = 5f;
    private Vector3 _startPosition = new(6, 1.1f, -30);

    public void ActivateView()
    {
        transform.localPosition = _startPosition;
        StartMove();
    }

    private void StartMove()
    {
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOLocalMoveX(_endPosX, _movingTime).SetEase(Ease.Linear));
    }

    public void SetValues(float movingTime, float endPosX)
    {
        _movingTime = movingTime;
        _endPosX = endPosX;
    }

    private void StopMove()
    {
        _moveSequence.Kill();
    }

    public void SetSectionActivatorEnterAction(Action action)
    {
        _sectionActivatorEnterAction = action;
    }

    public void SetSectionActivatorExitAction(Action action)
    {
        _sectionActivatorExitAction = action;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SectionActivator"))
        {
            StopMove();
            _sectionActivatorExitAction?.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SectionActivator"))
        {
            _sectionActivatorEnterAction?.Invoke();
        }
    }
}

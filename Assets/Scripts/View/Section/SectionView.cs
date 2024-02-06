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
    private Vector3 _startPosition;

    public void ActivateView(Vector3 startPos)
    {
        _startPosition = startPos;
        transform.position = _startPosition;
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

    public void StopMove()
    {
        _moveSequence.Kill();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).TryGetComponent(out IView view);
            view?.DeactivateView();
        }
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

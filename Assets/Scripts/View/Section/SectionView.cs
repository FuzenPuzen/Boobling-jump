using UnityEngine;
using DG.Tweening;
using System;
using UnityEditor;

public class SectionView : MonoBehaviour
{
    private Action _sectionActivatorExitAction;
    private Action _sectionActivatorEnterAction;
    private Sequence _moveSequence;
    private float _endPosX = -60f;
    private float _movingTime = 7.2f;
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
        if (other.GetComponent<SectionActivator>())
        {
            Debug.Log("OnTriggerExit", gameObject);
            
            StopMove();
            _sectionActivatorExitAction?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SectionActivator>())
        {            
            Debug.Log("OnTriggerEnter", other.gameObject);
            _sectionActivatorEnterAction?.Invoke();
        }
    }
}

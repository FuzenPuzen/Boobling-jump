using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System;
using System.Collections;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _playerModel;

    private IPlayerBehavior _currentBehavior;

    private Action playerDieAction;

    private DG.Tweening.Sequence _looseSequence;


    public void SetNewBehavior(IPlayerBehavior playerBehavior)
    {
        _currentBehavior = playerBehavior;
        _currentBehavior.StartBehavior();
    }

    public void Start()
    {
        _currentBehavior = new PlayerJumpBehavior(this);
        _currentBehavior.StartBehavior();
    }

    private void Update()
    {
        _currentBehavior.UpdateBehavior();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(DieCoroutine());
            Time.timeScale = 0.01f;         
        }
    }

    internal Transform GetPlayerModel()
    {
        return _playerModel;
    }

    #region die actions

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        _looseSequence = DOTween.Sequence();
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        _looseSequence.Append(transform.DOMove(new(6.3f, 9.95f, 10.5f),0.2f).SetEase(Ease.Linear));
        _looseSequence.Join(transform.DOLocalRotate(Vector3.zero,0.2f).SetEase(Ease.Linear));
        _looseSequence.Join(_playerModel.DOLocalRotate(new(80.24f, 2.87f, -85.43f), 0.2f).SetEase(Ease.Linear));
        _looseSequence.OnComplete(DieAction);
    }


    private void DieAction()
    {
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        playerDieAction?.Invoke();
    }

    public void SetActionOnPlayerDie(Action action)
    {
        playerDieAction = action;
    }

    #endregion

}

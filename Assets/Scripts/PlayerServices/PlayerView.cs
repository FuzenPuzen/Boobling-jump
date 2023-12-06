using UnityEngine;
using DG.Tweening;
using System;
using System.Collections;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _playerModel;

    private IPlayerBehavior _currentBehavior;

    private Action playerDieAction;


    public void SetNewBehavior(IPlayerBehavior playerBehavior)
    {
        _currentBehavior = playerBehavior;
        _currentBehavior.StartBehavior();
    }

    public void Start()
    {
        SetNewBehavior(new PlayerJumpBehavior(this, 10f));
        StartCoroutine(TestTimer());
    }

    private void Update()
    {
        _currentBehavior.UpdateBehavior();
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        _currentBehavior.ColliderBehavior(other);
    }

    internal Transform GetPlayerModel()
    {
        return _playerModel;
    }

    #region die actions
  

    public void SetActionOnPlayerDie(Action action)
    {
        playerDieAction = action;
    }

    #endregion

    private IEnumerator TestTimer()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Timer end");
        _currentBehavior.StopBehavior();
        SetNewBehavior(new PlayerRollBehavior(this, 10f));
        yield return new WaitForSeconds(30f);
        _currentBehavior.StopBehavior();
        SetNewBehavior(new PlayerJumpBehavior(this, 10f));
    }

}

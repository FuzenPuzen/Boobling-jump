using DG.Tweening;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerBehaviorService : IPlayerBehaviorService
{
    private IPlayerBehavior _currentBehavior;
    private PlayerView _playerView;
    private IFabric _fabric;

    private List<IPlayerBehavior> _playerBehaviors = new();

    private Sequence _timerSequence;
    private float _behaviourTime;
    private Action _onEndAction;

    public void SetBehavior<T>() where T : IPlayerBehavior
    {
        if (_currentBehavior != null)
            _currentBehavior.StopBehavior();

        _currentBehavior = _playerBehaviors.OfType<T>().FirstOrDefault();
        _playerView.SetNewBehavior(_currentBehavior);

        MonoBehaviour.print(_currentBehavior);
    }

    [Inject]
    public void Constructor(IFabric fabric)
    {
        _fabric = fabric;
    }

    public void ActivateService()
    {
        SpawnPlayer();
        _playerBehaviors.Add(new PlayerRollBehavior(_playerView, 10f));
        _playerBehaviors.Add(new PlayerSuperJumpBehavior(_playerView, 10f));
        _playerBehaviors.Add(new PlayerBasicJumpBehaviour(_playerView));
    }

    private void SpawnPlayer()
    {
        _playerView = _fabric.SpawnObjectAndGetType<PlayerView>(new(9.5f, 0, 0));
    }

    public void StartBehaviourTimer()
    {
        _timerSequence = DOTween.Sequence();
        _timerSequence.AppendInterval(_behaviourTime);
        _timerSequence.OnComplete(EndBehaviorTimer);
    }

    public void SetBehaviorEndAction(Action action)
    {
        _onEndAction = action;
    }

    private void EndBehaviorTimer()
    {
        _onEndAction?.Invoke();
    }
}

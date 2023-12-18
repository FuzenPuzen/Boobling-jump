using DG.Tweening;
using System.Collections.Generic;
using System;
using System.Linq;
using Zenject;
using UnityEngine;

public class PlayerBehaviorService : IPlayerBehaviorService
{
    private IPlayerBehavior _currentBehavior;
    private PlayerView _playerView;
    private IFabric _fabric;

    private IPlayerBehaviourData _playerBehaviourData;
    private List<IPlayerBehavior> _playerBehaviors = new();
    private IPlayerBehaviourStorageData _playerBehaviourStorageData;

    private Sequence _timerSequence;
    private Action _onEndAction;

    public void SetBehavior<T>() where T : IPlayerBehavior
    {
        if (_currentBehavior != null)
        {
            _currentBehavior.StopBehavior();
            MonoBehaviour.print("Stop Behaviour: " + _currentBehavior);
        }
        _currentBehavior = _playerBehaviors.OfType<T>().FirstOrDefault();
        _playerBehaviourData = _playerBehaviourStorageData.GetPlayerBehaviourData(_currentBehavior.GetBehaviourDataType());
        _currentBehavior.SetBehaviourData(_playerBehaviourData);
        MonoBehaviour.print("New Behaviour: " + _currentBehavior);
        _playerView.SetNewBehavior(_currentBehavior);
        StartBehaviourTimer();
    }

    [Inject]
    public void Constructor(IFabric fabric, IPlayerBehaviourStorageData playerBehaviourStorageData)
    {
        _playerBehaviourStorageData = playerBehaviourStorageData;
        _fabric = fabric;
    }

    public void ActivateService()
    {
        SpawnPlayer();
        _playerBehaviors.Add(new PlayerRollBehavior(_playerView));
        _playerBehaviors.Add(new PlayerSuperJumpBehavior(_playerView));
        _playerBehaviors.Add(new PlayerSimpleJumpBehaviour(_playerView));
    }

    private void SpawnPlayer()
    {
        _playerView = _fabric.SpawnObjectAndGetType<PlayerView>(new(9.5f, 0, 0));
    }

    public void StartBehaviourTimer()
    {
        MonoBehaviour.print("Behaviour duration: " + _playerBehaviourData.GetDuration());
        _timerSequence = DOTween.Sequence();
        _timerSequence.AppendInterval(_playerBehaviourData.GetDuration());
        _timerSequence.OnComplete(EndBehaviorTimer);
    }

    public void SetActionEndBehavior(Action action)
    {
        _onEndAction = action;
    }

    private void EndBehaviorTimer()
    {
        _onEndAction?.Invoke();
    }
}

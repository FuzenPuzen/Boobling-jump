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

    private IPlayerCurrentBehaviourData _playerCurrentBehaviourData;
    private List<IPlayerBehavior> _playerBehaviors = new();
    private IPlayerBehaviourData _playerBehaviourData;

    private Sequence _timerSequence;
    private Action _onEndAction;

    public void SetBehavior<T1, T2>() where T1 : IPlayerBehavior where T2 : IPlayerBehaviourData 
    {
        if (_currentBehavior != null)
            _currentBehavior.StopBehavior();
        _currentBehavior = _playerBehaviors.OfType<T1>().FirstOrDefault();
        _playerView.SetNewBehavior(_currentBehavior);
        _playerBehaviourData = _playerCurrentBehaviourData.GetPlayerCurrentBehaviourData<T2>();
        MonoBehaviour.print(_playerBehaviourData.GetDuration());
    }

    [Inject]
    public void Constructor(IFabric fabric, IPlayerCurrentBehaviourData playerCurrentBehaviourData)
    {
        _fabric = fabric;
        _playerCurrentBehaviourData = playerCurrentBehaviourData;
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
        _timerSequence.AppendInterval(_playerBehaviourData.GetDuration());
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

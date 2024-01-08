using DG.Tweening;
using System.Collections.Generic;
using System;
using System.Linq;
using Zenject;
using UnityEngine;

public class PlayerBehaviourService : IPlayerBehaviourService
{
    private IPlayerBehaviour _currentBehaviour;
    private PlayerView _playerView;
    private IViewFabric _fabric;

    private IPlayerBehaviourData _playerBehaviourData;
    private List<IPlayerBehaviour> _playerBehaviours = new();
    private IPlayerBehaviourStorageData _playerBehaviourStorageData;

    private Sequence _timerSequence;
    private Action _onEndAction;

    public void SetBehaviour<T>() where T : IPlayerBehaviour
    {
        if (_currentBehaviour != null)
            _currentBehaviour.StopBehaviour();

        _currentBehaviour = _playerBehaviours.OfType<T>().FirstOrDefault();
        if (typeof(T) == typeof(PlayerStartBehaviour))
        {
            var temp = (PlayerStartBehaviour)_currentBehaviour;
            temp.SetStartAction(EndBehaviourTimer);
        }

        _playerBehaviourData = _playerBehaviourStorageData.GetPlayerBehaviourData(_currentBehaviour.GetBehaviourDataType());
        _currentBehaviour.SetBehaviourData(_playerBehaviourData);

        _playerView.SetNewBehaviour(_currentBehaviour);
        StartBehaviourTimer();
    }

    [Inject]
    public void Constructor(IViewFabric fabric, IPlayerBehaviourStorageData playerBehaviourStorageData)
    {
        _playerBehaviourStorageData = playerBehaviourStorageData;
        _fabric = fabric;
    }

    public void ActivateService()
    {
        SpawnPlayer();
        _playerBehaviours.Add(new PlayerRollBehaviour(_playerView));
        _playerBehaviours.Add(new PlayerStartBehaviour(_playerView));
        _playerBehaviours.Add(new PlayerSuperJumpBehaviour(_playerView));
        _playerBehaviours.Add(new PlayerSimpleJumpBehaviour(_playerView));
    }

    private void SpawnPlayer()
    {
        _playerView = _fabric.SpawnObject<PlayerView>(new Vector3(4.83f, 1.24f, 0));
    }

    public void StartBehaviourTimer()
    {
        _timerSequence = DOTween.Sequence();
        _timerSequence.AppendInterval(_playerBehaviourData.GetDuration());
        _timerSequence.OnComplete(EndBehaviourTimer);
    }

    public void SetActionEndBehaviour(Action action)
    {
        _onEndAction = action;
    }

    private void EndBehaviourTimer()
    {
        _onEndAction?.Invoke();
    }
}

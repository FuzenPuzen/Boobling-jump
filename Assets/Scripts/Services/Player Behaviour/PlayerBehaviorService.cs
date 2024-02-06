using DG.Tweening;
using System.Collections.Generic;
using System;
using System.Linq;
using Zenject;
using UnityEngine;
using EventBus;

public class PlayerBehaviourService : IPlayerBehaviourService
{
    private IPlayerBehaviour _currentBehaviour;
    private PlayerView _playerView;
    private IViewFabric _fabric;

    private IPlayerBehaviourData _playerBehaviourData;
    private List<IPlayerBehaviour> _playerBehaviours = new();
    private IPlayerBehaviourStorageData _playerBehaviourStorageData;
    private IPlayerSkinDataManager _playerSkinDataManager;
    private IMarkerService _markerService;

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
            temp.SetStartAction(StartBehaviourEnd);
        }

        _playerBehaviourData = _playerBehaviourStorageData.GetPlayerBehaviourData(_currentBehaviour.GetBehaviourDataType());
        _currentBehaviour.SetBehaviourData(_playerBehaviourData);

        _playerView.SetNewBehaviour(_currentBehaviour);
    }

    [Inject]
    public void Constructor(IViewFabric fabric, IPlayerBehaviourStorageData playerBehaviourStorageData,
                            IPlayerSkinDataManager playerSkinDataManager,
                            IMarkerService markerService)
    {
        _markerService = markerService;
        _playerSkinDataManager = playerSkinDataManager;
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
        GameObject PlayerModel = _playerSkinDataManager.GetCurrentSkin().PlayerSkinSOData.SkinPrefab.transform.GetChild(0).gameObject;
        _playerView = _fabric.Init<PlayerView>(new Vector3(4.83f, 1.24f, 0));
        Transform parent = _markerService.GetTransformMarker<PlayerMarker>().transform;
        GameObject model = _fabric.Init(PlayerModel, parent);
        _playerView.SetPlayerModel(model.transform);
    }

    public void SetActionEndBehaviour(Action action)
    {
        _onEndAction = action;
    }

    private void StartBehaviourEnd()
    {
        EventBus<OnStartBehaviourEnd>.Raise();
    }
}

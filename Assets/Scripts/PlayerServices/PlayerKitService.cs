﻿using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerKitService :Iservice
{
    private PlayerKitView _playerKitView;
    private IFabric _fabric;

    [Inject]
    public void Constructor(IFabric fabric)
    {
        _fabric = fabric;        
    }

    public void ActivateService()
    {
        _playerKitView = _fabric.SpawnObjectAndGetType<PlayerKitView>(new(9.5f, 0, 0));
    }

    public void SetActionOnPlayerDie(Action action)
    {
        _playerKitView.SetActionOnPlayerDie(action);
    }

    public PlayerView GetPlayerView()
    {
        return _playerKitView.GetPlayerView();
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKitView : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;

    public PlayerView GetPlayerView()
    {
        return _playerView;
    }

    public void SetActionOnPlayerDie(Action action)
    {
        _playerView.SetActionOnPlayerDie(action);
    }

}

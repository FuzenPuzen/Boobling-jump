using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKitView : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;

    internal PlayerView GetPlayerView()
    {
        return _playerView;
    }
}

using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.PlayerService
{
    public class PlayerKitService
    {
        private PlayerKitView _playerKitView;
        private PlayerView _playerView;

        public Action ReachPlatform;

        [Inject]
        public void Constructor(PrefabsStorageService prefabsStorageService)
        {
            PlayerKitView playerKitViewPb = prefabsStorageService.GetPrefabByType<PlayerKitView>();
            _playerKitView = MonoBehaviour.Instantiate(playerKitViewPb);
            _playerView = _playerKitView.GetPlayerView();
        }

        private void ReachPlatfrom()
        {
            ReachPlatform?.Invoke();
        }


    }
}
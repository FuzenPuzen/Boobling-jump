using System.Collections.Generic;
using System.Linq;
using Zenject;

public class PlayerBehaviorService : IPlayerBehaviorService
{
    private IPlayerBehavior _currentBehavior;
    private PlayerKitService _playerKitService;
    private PlayerView _playerView;

    private List<IPlayerBehavior> _playerBehaviors = new();

    public void SetBehavior<T>() where T : IPlayerBehavior
    {
        if (_currentBehavior != null)
            _currentBehavior.StopBehavior();
        _currentBehavior = _playerBehaviors.OfType<T>().FirstOrDefault();
        //_playerView.SetNewBehavior(_currentBehavior);
    }

    [Inject]
    public void Constructor(PlayerKitService playerKitService)
    {
        _playerKitService = playerKitService;       
    }

    public void ActivateService()
    {
        _playerView = _playerKitService.GetPlayerView();
        _playerBehaviors.Add(new PlayerSuperJumpBehavior(_playerView, 10f));
        _playerBehaviors.Add(new PlayerJumpBehavior(_playerView, 10f));
    }
}

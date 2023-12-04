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
    }

    [Inject]
    public void Construct(PlayerKitService playerKitService)
    {
        _playerKitService = playerKitService;
        _playerView = _playerKitService.GetPlayerView();
    }

    public void ActivateService()
    {
        _playerBehaviors.Add(new PlayerSuperJumpBehavior(_playerView));
        _playerBehaviors.Add(new PlayerJumpBehavior(_playerView));
    }
}

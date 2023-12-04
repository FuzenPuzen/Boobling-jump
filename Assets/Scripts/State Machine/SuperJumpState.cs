using Zenject;

public class SuperJumpState : IBaseState
{
    private IPlayerBehaviorService _playerBehaviorService;

    public SuperJumpState(
                            IPlayerBehaviorService playerBehaviorService)
    {
        _playerBehaviorService = playerBehaviorService;
    }

    public void Enter()
    {
        _playerBehaviorService.ActivateService();
        _playerBehaviorService.SetBehavior<PlayerSuperJumpBehavior>();
    }

    public void Exit()
    {
        //do nothing
    }

    public void Update()
    {
        //do nothing
    }
}

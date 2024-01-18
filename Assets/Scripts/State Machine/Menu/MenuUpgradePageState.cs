using Zenject;

public class MenuUpgradePageState : IBaseState
{
    private MenuUpgradePageCanvasViewService _menuUpgradePageCanvasViewService;

    private IService _playerBehaviourDataManager;
    [Inject]
    public void Constructor(MenuUpgradePageCanvasViewService menuUpgradePageCanvasViewService,
                            IPlayerBehaviourDataManager playerBehaviourDataManager)
    {
        _playerBehaviourDataManager = playerBehaviourDataManager;
        _menuUpgradePageCanvasViewService = menuUpgradePageCanvasViewService;
    }

    public void Enter()
    {
        _playerBehaviourDataManager.ActivateService();
        _menuUpgradePageCanvasViewService.ActivateService();
    }

    public void Exit()
    {
        _menuUpgradePageCanvasViewService.HideView();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}

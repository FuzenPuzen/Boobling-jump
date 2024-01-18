using EventBus;
using Zenject;

public class MenuUpgradePageState : IBaseState
{
    private MenuUpgradePageCanvasViewService _menuUpgradePageCanvasViewService;
    EventBinding<OnClickMenu> _onClickMenu;
    private MenuStateMachine _menuStateMachine;
    private BackButtonCanvasViewService _backButtonCanvasViewService;

    private IService _playerBehaviourDataManager;
    [Inject]
    public void Constructor(MenuUpgradePageCanvasViewService menuUpgradePageCanvasViewService,
                            BackButtonCanvasViewService backButtonCanvasViewService,
                            IPlayerBehaviourDataManager playerBehaviourDataManager,
                            MenuStateMachine menuStateMachine)
    {
        _backButtonCanvasViewService = backButtonCanvasViewService;
        _menuStateMachine = menuStateMachine;
        _playerBehaviourDataManager = playerBehaviourDataManager;
        _menuUpgradePageCanvasViewService = menuUpgradePageCanvasViewService;
    }

    public void Enter()
    {
        _playerBehaviourDataManager.ActivateService();
        _menuUpgradePageCanvasViewService.ActivateService();
        _onClickMenu = new EventBinding<OnClickMenu>(ToMenu);
        _backButtonCanvasViewService.ShowView();
    }

    public void Exit()
    {
        _menuUpgradePageCanvasViewService.HideView();
        _backButtonCanvasViewService.HideView();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }

    private void ToMenu()
    {
        _menuStateMachine.SetState<MenuMainPageState>();
    }

}

using EventBus;
using Zenject;

public class MenuUpgradePageState : IBaseState
{
    private MenuUpgradePageCanvasViewService _menuUpgradePageCanvasViewService;
    private EventBinding<OnClickMenu> _onClickMenu;
    private MenuStateMachine _menuStateMachine;
    private BackButtonCanvasViewService _backButtonCanvasViewService;

    
    [Inject]
    public void Constructor(MenuUpgradePageCanvasViewService menuUpgradePageCanvasViewService,
                            BackButtonCanvasViewService backButtonCanvasViewService,
                            
                            MenuStateMachine menuStateMachine)
    {
        _backButtonCanvasViewService = backButtonCanvasViewService;
        _menuStateMachine = menuStateMachine;
        
        _menuUpgradePageCanvasViewService = menuUpgradePageCanvasViewService;
        _onClickMenu = new(ToMenu);
    }

    public void Enter()
    {
        _menuUpgradePageCanvasViewService.ShowView();       
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

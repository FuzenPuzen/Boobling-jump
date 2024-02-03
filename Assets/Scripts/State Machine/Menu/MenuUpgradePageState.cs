using EventBus;
using Zenject;

public class MenuUpgradePageState : IBaseState
{
    private MenuUpgradePageCanvasViewService _menuUpgradePageCanvasViewService;
    private EventBinding<OnClickMenu> _onClickMenu;
    private MenuStateMachine _menuStateMachine;

    
    [Inject]
    public void Constructor(MenuUpgradePageCanvasViewService menuUpgradePageCanvasViewService,
                            
                            MenuStateMachine menuStateMachine)
    {
        _menuStateMachine = menuStateMachine;
        
        _menuUpgradePageCanvasViewService = menuUpgradePageCanvasViewService;       
    }

    public void Enter()
    {
        _onClickMenu = new(ToMenu);
        _menuUpgradePageCanvasViewService.ShowView();       
    }

    public void Exit()
    {
        _onClickMenu.Remove(ToMenu);
        _menuUpgradePageCanvasViewService.HideView();
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

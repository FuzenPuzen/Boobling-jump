using EventBus;
using Zenject;

public class MenuSkinShopPageState : IBaseState
{
    private MenuSkinShopPageCanvasViewService _menuSkinShopPageCanvasViewService;
    private EventBinding<OnClickMenu> _onClickMenu;
    private MenuStateMachine _menuStateMachine;


    [Inject]
    public void Constructor(MenuSkinShopPageCanvasViewService menuSkinShopPageCanvasViewService,
                            MenuStateMachine menuStateMachine)
    {
        _menuSkinShopPageCanvasViewService = menuSkinShopPageCanvasViewService;
        _menuStateMachine = menuStateMachine;      
    }

    public void Enter()
    {
        _onClickMenu = new(ToMenu);
        _menuSkinShopPageCanvasViewService.ShowView();
    }

    public void Exit()
    {
        _onClickMenu.Remove(ToMenu);
        _menuSkinShopPageCanvasViewService.HideView();
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

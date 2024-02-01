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
        _onClickMenu = new(ToMenu);
    }

    public void Enter()
    {
        _menuSkinShopPageCanvasViewService.ShowView();
    }

    public void Exit()
    {
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

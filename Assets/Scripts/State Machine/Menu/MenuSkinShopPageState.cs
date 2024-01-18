using EventBus;
using Zenject;

public class MenuSkinShopPageState : IBaseState
{
    private MenuSkinShopPageCanvasViewService _menuSkinShopPageCanvasViewService;
    private BackButtonCanvasViewService _backButtonCanvasViewService;
    EventBinding<OnClickMenu> _onClickMenu;
    private MenuStateMachine _menuStateMachine;

    [Inject]
    public void Constructor(MenuSkinShopPageCanvasViewService menuSkinShopPageCanvasViewService,
                            BackButtonCanvasViewService backButtonCanvasViewService,
                            MenuStateMachine menuStateMachine)
    {
        _backButtonCanvasViewService = backButtonCanvasViewService;
        _menuSkinShopPageCanvasViewService = menuSkinShopPageCanvasViewService;
        _menuStateMachine = menuStateMachine;
    }

    public void Enter()
    {
        _menuSkinShopPageCanvasViewService.ActivateService();
        _onClickMenu = new EventBinding<OnClickMenu>(ToMenu);
        _backButtonCanvasViewService.ShowView();
    }

    public void Exit()
    {
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

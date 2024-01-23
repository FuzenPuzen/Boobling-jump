using EventBus;
using Zenject;

public class MenuSkinShopPageState : IBaseState
{
    private MenuSkinShopPageCanvasViewService _menuSkinShopPageCanvasViewService;
    private BackButtonCanvasViewService _backButtonCanvasViewService;
    private EventBinding<OnClickMenu> _onClickMenu;
    private MenuStateMachine _menuStateMachine;
    private IPlayerSkinDataManager _playerSkinDataManager;

    [Inject]
    public void Constructor(MenuSkinShopPageCanvasViewService menuSkinShopPageCanvasViewService,
                            BackButtonCanvasViewService backButtonCanvasViewService,
                            MenuStateMachine menuStateMachine,
                            IPlayerSkinDataManager playerSkinDataManager)
    {
        _playerSkinDataManager = playerSkinDataManager;
        _backButtonCanvasViewService = backButtonCanvasViewService;
        _menuSkinShopPageCanvasViewService = menuSkinShopPageCanvasViewService;
        _menuStateMachine = menuStateMachine;
        _onClickMenu = new(ToMenu);
    }

    public void Enter()
    {
        _playerSkinDataManager.ActivateService();
        _menuSkinShopPageCanvasViewService.ActivateService();
        _backButtonCanvasViewService.ShowView();
    }

    public void Exit()
    {
        _menuSkinShopPageCanvasViewService.HideView();
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

using EventBus;
using Zenject;

using UnityEngine;

public class MenuMainPageState : IBaseState
{
    EventBinding<OnClickGame> _onClickGame;
    EventBinding<OnClickUpgrade> _onClickUpgrade;
    EventBinding<OnClickSkinShop> _onClickSkinShop;

    private MenuStateMachine _menuStateMachine;
    private MenuMainPageCanvasViewService _menuMainPageViewService;
    private NavigationCanvasViewService _navigationCanvasViewService;
    private NavigationPanelViewService _navigationPanelViewService;


    [Inject]
    public void Constructor(MenuMainPageCanvasViewService menuMainPageViewService,                          
                            MenuStateMachine menuStateMachine,
                            NavigationCanvasViewService navigationCanvasViewService)
    {
        _navigationCanvasViewService = navigationCanvasViewService;
        _menuStateMachine = menuStateMachine;
        _menuMainPageViewService = menuMainPageViewService;
    }

    public void Enter()
    {
        _onClickGame = new(OnGameClickEvent);
        _onClickUpgrade = new(OnUpgradeClickEvent);
        _onClickSkinShop = new(OnSkinShopClickEvent);
        _menuMainPageViewService.ShowView();
        _navigationCanvasViewService.HideBackButton();
        _navigationCanvasViewService.SetPanelName(PanelName.Menu);
    }

    public void Exit()
    {
        _onClickGame.Remove(OnGameClickEvent);
        _onClickUpgrade.Remove(OnUpgradeClickEvent);
        _onClickSkinShop.Remove(OnSkinShopClickEvent);
        _menuMainPageViewService.HideView();
        _navigationCanvasViewService.ShowBackButton();
    }

    public void Update()
    {
        
    }

    public void OnGameClickEvent()
    {
        _menuStateMachine.SetState<MenuLastState>();
    }

    public void OnUpgradeClickEvent()
    {
        _navigationCanvasViewService.SetPanelName(PanelName.Upgrage);
        _menuStateMachine.SetState<MenuUpgradePageState>();
    }    

    public void OnSkinShopClickEvent()
    {
        _navigationCanvasViewService.SetPanelName(PanelName.Skin);
        _menuStateMachine.SetState<MenuSkinShopPageState>();
    }
}

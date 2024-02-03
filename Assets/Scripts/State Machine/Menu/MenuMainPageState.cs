using EventBus;
using Zenject;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuMainPageState : IBaseState
{
    EventBinding<OnClickGame> _onClickGame;
    EventBinding<OnClickUpgrade> _onClickUpgrade;
    EventBinding<OnClickSkinShop> _onClickSkinShop;

    private MenuStateMachine _menuStateMachine;
    private MenuMainPageCanvasViewService _menuMainPageViewService;


    [Inject]
    public void Constructor(MenuMainPageCanvasViewService menuMainPageViewService,                          
                            MenuStateMachine menuStateMachine)
    {
        _menuStateMachine = menuStateMachine;
        _menuMainPageViewService = menuMainPageViewService;
    }

    public void Enter()
    {
        _onClickGame = new(OnGameClickEvent);
        _onClickUpgrade = new(OnUpgradeClickEvent);
        _onClickSkinShop = new(OnSkinShopClickEvent);
        _menuMainPageViewService.ShowView();
    }

    public void Exit()
    {
        _onClickGame.Remove(OnGameClickEvent);
        _onClickUpgrade.Remove(OnUpgradeClickEvent);
        _onClickSkinShop.Remove(OnSkinShopClickEvent);
        _menuMainPageViewService.HideView();
    }

    public void Update()
    {
        
    }

    public void OnGameClickEvent()
    {
        SceneManager.LoadScene("SessionSceneV1");
    }

    public void OnUpgradeClickEvent()
    {
        _menuStateMachine.SetState<MenuUpgradePageState>();
    }    

    public void OnSkinShopClickEvent()
    {
        _menuStateMachine.SetState<MenuSkinShopPageState>();
    }
}

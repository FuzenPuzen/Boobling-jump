using EventBus;
using Zenject;
using UnityEngine.SceneManagement;

public class MenuMainPageState : IBaseState
{
    EventBinding<OnClickGame> _onClickGame;
    EventBinding<OnClickUpgrade> _onClickUpgrade;

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
        _menuMainPageViewService.ActivateService();

        _onClickGame = new EventBinding<OnClickGame>(OnGameClickEvent);
        _onClickUpgrade = new EventBinding<OnClickUpgrade>(OnUpgradeClickEvent);
    }

    public void Exit()
    {
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
}

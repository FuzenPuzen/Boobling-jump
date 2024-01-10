using EventBus;
using Zenject;
using UnityEngine.SceneManagement;

public class MenuMainPageState : IBaseState
{
    EventBinding<OnClickGame> _onClickGame;

    [Inject]
    public void Constructor()
    {
        
    }

    public void Enter()
    {
        _onClickGame = new EventBinding<OnClickGame>(OnGameClickEvent);
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }

    public void OnGameClickEvent()
    {
        SceneManager.LoadScene("SessionSceneV1");
    }
}

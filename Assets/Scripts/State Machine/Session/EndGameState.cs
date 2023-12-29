using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class EndGameState : IBaseState
{
    private CurrentScoreService _scoreService;
    private RecordService _recordService;
    private EndPanelService _endPanelService;
    private IScoreDataManager _scoreDataManager;

    [Inject]
    public void Constructor(
        CurrentScoreService scoreService,
        RecordService recordService,
        EndPanelService endPanelService,
        IScoreDataManager scoreDataManager
        )
    {
        _endPanelService = endPanelService;
        _recordService = recordService;
        _scoreService = scoreService;
        _scoreDataManager = scoreDataManager;
    }

    public void Enter()
    {
        _endPanelService.ActivateService();
        _endPanelService.SetActionOnRestartButtonClick(RestartScene);
        _recordService.SetRecord(_scoreDataManager.GetCurrentScore());
        _scoreService.HideView();
        _recordService.HideView();       
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}

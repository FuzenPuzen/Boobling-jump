using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class EndGameState : IBaseState
{
    private ScoreService _scoreService;
    private RecordService _recordService;
    private EndPanelService _endPanelService;

    [Inject]
    public EndGameState(
        ScoreService scoreService,
        RecordService recordService,
        EndPanelService endPanelService
        )
    {
        _endPanelService = endPanelService;
        _recordService = recordService;
        _scoreService = scoreService;
    }

    public void Enter()
    {
        _endPanelService.ActivateService();
        _endPanelService.SetRestartInstruction(RestartScene);
        _recordService.SetRecord(_scoreService.GetScore());
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

using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class EndGameState : IBaseState
{
    private ScoreService _scoreService;
    private RecordService _recordService;

    [Inject]
    public EndGameState(ScoreService scoreService, RecordService recordService)
    {
        _recordService = recordService;
        _scoreService = scoreService;
    }

    public void Enter()
    {
        _recordService.SetRecord(_scoreService.GetScore());
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

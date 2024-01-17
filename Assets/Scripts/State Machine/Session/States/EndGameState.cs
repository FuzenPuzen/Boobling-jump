using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class EndGameState : IBaseState
{
    private CurrentScorePanelService _scoreService;
    private RecordScorePanelService _recordService;
    private EndPanelService _endPanelService;
    private IScoreDataManager _scoreDataManager;
    private GiftCollectorViewService _giftCollectorViewService;
    private RollBonusBlenderViewService _rollBonusBlenderViewService;
    private SuperJumpBonusBlenderViewService _superJumpBonusBlenderViewService;

    [Inject]
    public void Constructor(
        CurrentScorePanelService scoreService,
        RecordScorePanelService recordService,
        EndPanelService endPanelService,
        IScoreDataManager scoreDataManager,
        GiftCollectorViewService giftCollectorViewService,
        RollBonusBlenderViewService rollBonusBlenderViewService,
        SuperJumpBonusBlenderViewService superJumpBonusBlenderViewService
        )
    {
        _endPanelService = endPanelService;
        _recordService = recordService;
        _scoreService = scoreService;
        _scoreDataManager = scoreDataManager;
        _giftCollectorViewService = giftCollectorViewService;
        _rollBonusBlenderViewService = rollBonusBlenderViewService;
        _superJumpBonusBlenderViewService = superJumpBonusBlenderViewService;
    }

    public void Enter()
    {
        _scoreService.DeactivateScoreChange();
        _endPanelService.ActivateService();
        _endPanelService.SetActionOnRestartButtonClick(RestartScene);
        _giftCollectorViewService.DeactivateService();
        _superJumpBonusBlenderViewService.DeactivateService();
        _rollBonusBlenderViewService.DeactivateService();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}

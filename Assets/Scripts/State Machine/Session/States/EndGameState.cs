using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class EndGameState : IBaseState
{
    private CurrentScorePanelService _scoreService;
    private RecordScorePanelService _recordService;
    private IScoreDataManager _scoreDataManager;
    private GiftCollectorViewService _giftCollectorViewService;
    private RollBonusBlenderViewService _rollBonusBlenderViewService;
    private SuperJumpBonusBlenderViewService _superJumpBonusBlenderViewService;
    private EndPageViewService _endPageViewService;

    [Inject]
    public void Constructor(
        CurrentScorePanelService scoreService,
        RecordScorePanelService recordService,
        IScoreDataManager scoreDataManager,
        GiftCollectorViewService giftCollectorViewService,
        RollBonusBlenderViewService rollBonusBlenderViewService,
        SuperJumpBonusBlenderViewService superJumpBonusBlenderViewService,
        EndPageViewService endPageViewService
        )
    {
        _recordService = recordService;
        _scoreService = scoreService;
        _scoreDataManager = scoreDataManager;
        _giftCollectorViewService = giftCollectorViewService;
        _rollBonusBlenderViewService = rollBonusBlenderViewService;
        _superJumpBonusBlenderViewService = superJumpBonusBlenderViewService;
        _endPageViewService = endPageViewService;
    }

    public void Enter()
    {
        _endPageViewService.ActivateService();
        _scoreService.DeactivateScoreChange();
        _giftCollectorViewService.DeactivateService();
        _superJumpBonusBlenderViewService.DeactivateService();
        _rollBonusBlenderViewService.DeactivateService();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}

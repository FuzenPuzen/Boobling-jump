using EventBus;
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
    private EventBinding<OnRestart> _onRestartBinding;
    private EventBinding<OnOpenMenu> _onOpenMenuBinding;
    private SessionStateMachine _statemachine;

    [Inject]
    public void Constructor(
        CurrentScorePanelService scoreService,
        RecordScorePanelService recordService,
        IScoreDataManager scoreDataManager,
        GiftCollectorViewService giftCollectorViewService,
        RollBonusBlenderViewService rollBonusBlenderViewService,
        SuperJumpBonusBlenderViewService superJumpBonusBlenderViewService,
        EndPageViewService endPageViewService,
        SessionStateMachine sessionStateMachine
        )
    {
        _statemachine = sessionStateMachine;
        _recordService = recordService;
        _scoreService = scoreService;
        _scoreDataManager = scoreDataManager;
        _giftCollectorViewService = giftCollectorViewService;
        _rollBonusBlenderViewService = rollBonusBlenderViewService;
        _superJumpBonusBlenderViewService = superJumpBonusBlenderViewService;
        _endPageViewService = endPageViewService;
        _onRestartBinding = new(RestartScene);
        _onOpenMenuBinding = new(OpenMainMenu);
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
        LoaderSceneService.Instance.SetBufScene(GameScenes.SessionScene);
        _statemachine.SetState<SessionLastState>();
    }
    public void OpenMainMenu()
    {
        LoaderSceneService.Instance.SetBufScene(GameScenes.MenuScene);
        _statemachine.SetState<SessionLastState>();
    }
    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}

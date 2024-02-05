using EventBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TutorialFinishState : IBaseState
{
    private CurrentScorePanelService _scoreService;
    private GiftCollectorViewService _giftCollectorViewService;
    private RollBonusBlenderViewService _rollBonusBlenderViewService;
    private SuperJumpBonusBlenderViewService _superJumpBonusBlenderViewService;
    private EventBinding<OnRestart> _onRestartBinding;
    private EventBinding<OnOpenMenu> _onOpenMenuBinding;
    private SessionStateMachine _statemachine;
    private ISessionTypeDataManager _sessionTypeDataManager;
    private TutorialEndPanelViewService _tutorialEndPanelViewService;

    [Inject]
    public void Constructor(
        CurrentScorePanelService scoreService,
        GiftCollectorViewService giftCollectorViewService,
        RollBonusBlenderViewService rollBonusBlenderViewService,
        SuperJumpBonusBlenderViewService superJumpBonusBlenderViewService,
        SessionStateMachine sessionStateMachine,
        ISessionTypeDataManager sessionTypeDataManager,
        TutorialEndPanelViewService tutorialEndPanelViewService
        )
    {
        _tutorialEndPanelViewService = tutorialEndPanelViewService;
        _sessionTypeDataManager = sessionTypeDataManager;
        _statemachine = sessionStateMachine;
        _scoreService = scoreService;
        _giftCollectorViewService = giftCollectorViewService;
        _rollBonusBlenderViewService = rollBonusBlenderViewService;
        _superJumpBonusBlenderViewService = superJumpBonusBlenderViewService;
    }
    public void Enter()
    {
        _onRestartBinding = new(RestartScene);
        _onOpenMenuBinding = new(OpenMainMenu);
        _sessionTypeDataManager.SaveGameType(false);

        _tutorialEndPanelViewService.ActivateService();
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
        _onRestartBinding.Remove(RestartScene);
        _onOpenMenuBinding.Remove(OpenMainMenu);
    }

    public void Update()
    {

    }
}

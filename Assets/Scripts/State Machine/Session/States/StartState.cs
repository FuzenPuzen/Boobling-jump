using UnityEngine;
using Zenject;

public class StartState : IBaseState
{
    private SessionStateMachine _statemachine;
    private IService _sectionsService;
    private IService _currentScoreService;
    private IService _recordScoreService;
    private IService _giftScoreService;
    private IService _giftService;
    private IRepaintService _repaintService;


    [Inject]
    public void Constructor(
                        ISectionBehavioursService sectionsService,
                        SessionStateMachine statemachine,
                        CurrentScorePanelService scoreService,
                        RecordScorePanelService recordScoreService,
                        GiftScorePanelService giftScorePanelService,
                        GiftService giftService,
                        IRepaintService repaintService
                     )
    {
        _currentScoreService = scoreService;
        _sectionsService = sectionsService;
        _statemachine = statemachine;
        _recordScoreService = recordScoreService;
        _giftScoreService = giftScorePanelService;
        _giftService = giftService;
        _repaintService = repaintService;

    }

    public void Enter()
    {
        _currentScoreService.ActivateService();
        _recordScoreService.ActivateService();
        _giftScoreService.ActivateService();
        _sectionsService.ActivateService();
        _giftService.ActivateService();
        _repaintService.RepaintAll();
        _statemachine.SetState<GameState>();
    }

    public void Exit()
    {
        //do nothing
    }

    public void Update()
    {
        //do nothing
    }
  
}

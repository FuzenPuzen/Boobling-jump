using System;
using Zenject;

public class EndPanelService : IService
{

    private EndPanelView _endPanelview;
    private CurrentScorePanelService _scoreService;
    private RecordScorePanelService _recordService;
    private IViewFabric _fabric;
    private IScoreDataManager _scoreDataManager;

    [Inject]
    public void Constructor(IViewFabric fabric, IScoreDataManager scoreDataManager, CurrentScorePanelService scoreService, RecordScorePanelService recordService)
    {
        _recordService = recordService;
        _scoreService = scoreService;
        _fabric = fabric;      
        _scoreDataManager = scoreDataManager;
    }

    public void ActivateService()
    {
        _endPanelview = _fabric.SpawnObject<EndPanelView>();
        _endPanelview.SetScoreAndRecord(_scoreDataManager.GetCurrentScore(), _scoreDataManager.GetCurrentScore());
        _endPanelview.StartAction();
    }

    public void SetActionOnRestartButtonClick(Action action)
    {
        _endPanelview.SetActionOnRestartButtonClick(action);
    }

}

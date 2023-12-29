using System;
using Zenject;

public class EndPanelService : Iservice
{

    private EndPanelView _endPanelview;
    private CurrentScoreService _scoreService;
    private RecordService _recordService;
    private IFabric _fabric;
    private IScoreDataManager _scoreDataManager;

    [Inject]
    public void Constructor(IFabric fabric, IScoreDataManager scoreDataManager, CurrentScoreService scoreService, RecordService recordService)
    {
        _recordService = recordService;
        _scoreService = scoreService;
        _fabric = fabric;      
        _scoreDataManager = scoreDataManager;
    }

    public void ActivateService()
    {
        _endPanelview = _fabric.SpawnObjectAndGetType<EndPanelView>();
        _endPanelview.SetScoreAndRecord(_scoreDataManager.GetCurrentScore(), _scoreDataManager.GetCurrentScore());
        _endPanelview.StartAction();
    }

    public void SetActionOnRestartButtonClick(Action action)
    {
        _endPanelview.SetActionOnRestartButtonClick(action);
    }

}

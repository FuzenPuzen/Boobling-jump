using System;
using Zenject;

public class EndPanelService : Iservice
{

    private EndPanelView _endPanelview;
    private ScoreService _scoreService;
    private RecordService _recordService;
    private IFabric _fabric;

    [Inject]
    public void Constructor(IFabric fabric, ScoreService scoreService, RecordService recordService)
    {
        _recordService = recordService;
        _scoreService = scoreService;
        _fabric = fabric;      
    }

    public void ActivateService()
    {
        _endPanelview = _fabric.SpawnObjectAndGetType<EndPanelView>();
        _endPanelview.SetScoreAndRecord(_scoreService.GetScore(), _recordService.GetRecord());
        _endPanelview.StartAction();
    }

    public void SetActionOnRestartButtonClick(Action action)
    {
        _endPanelview.SetActionOnRestartButtonClick(action);
    }

}

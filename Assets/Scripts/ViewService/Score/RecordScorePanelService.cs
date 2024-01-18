using UnityEngine;
using Zenject;

public class RecordScorePanelService: IService
{
    private IScoreDataManager _scoreDataManager;
    private IMarkerService _markerService;
    private RecordScorePanelView _recordView;
    private int _recordScore;
    private IViewFabric _fabric;

    public void ActivateService()
    {
        Transform parent = _markerService.GetTransformMarker<RecordScorePanelPosMarker>().transform;
        _recordView = _fabric.Init<RecordScorePanelView>(parent);
        _scoreDataManager.RecordChanged += UpdateView;
        _recordScore = _scoreDataManager.GetRecordScore();
        UpdateView(_recordScore);
    }

    [Inject]
    public void Constructor(
        IViewFabric fabric,
        IScoreDataManager scoreDataManager, IMarkerService markerService)
    {
        _fabric = fabric;
        _scoreDataManager = scoreDataManager;
        _markerService = markerService;
    }

    public void HideView()
    {
        _recordView.HideView();
    }

    private void UpdateView(int record)
    {
        _recordView.UpdateView(record);
    }
}

using UnityEngine;
using Zenject;

public class RecordScorePanelService: IService
{
    private IScoreDataManager _scoreDataManager;
    private IRoomViewManager _roomViewManager;
    private RecordScorePanelView _recordView;
    private int _recordScore;
    private IViewFabric _fabric;

    public void ActivateService()
    {
        _recordView = _fabric.SpawnObject<RecordScorePanelView>(_roomViewManager.GetRecordScorePos());
        _scoreDataManager.RecordChanged += UpdateView;
        _recordScore = _scoreDataManager.GetRecordScore();
        UpdateView(_recordScore);
    }

    [Inject]
    public void Constructor(
        IViewFabric fabric,
        IScoreDataManager scoreDataManager, IRoomViewManager roomViewManager)
    {
        _fabric = fabric;
        _scoreDataManager = scoreDataManager;
        _roomViewManager = roomViewManager;
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

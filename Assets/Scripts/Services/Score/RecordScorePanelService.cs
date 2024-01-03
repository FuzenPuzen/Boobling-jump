using UnityEngine;
using Zenject;

public class RecordScorePanelService: Iservice
{
    private IScoreDataManager _scoreDataManager;
    private RecordScorePanelView _recordView;
    private int _recordScore;
    private IFabric _fabric;
    private Vector3 spawnPanelPos = new(6.75f, 3.75f, 2.75f);

    public void ActivateService()
    {
        _recordView = _fabric.SpawnObjectAndGetType<RecordScorePanelView>(spawnPanelPos);
        _scoreDataManager.RecordChanged += UpdateView;
        _recordScore = _scoreDataManager.GetRecordScore();
        UpdateView(_recordScore);
    }

    [Inject]
    public void Constructor(
        IFabric fabric,
        IScoreDataManager scoreDataManager)
    {
        _fabric = fabric;
        _scoreDataManager = scoreDataManager;
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

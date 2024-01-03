using UnityEngine;
using Zenject;

public class RecordScorePanelService: Iservice
{
    private IScoreDataManager _scoreDataManager;
    private RecordScorePanelView _recordView;
    private int _recordScore;
    private IFabric _fabric;

    public void ActivateService()
    {
        MonoBehaviour.print("TEST ACTIVATE");
        _recordView = _fabric.SpawnObjectAndGetType<RecordScorePanelView>();
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

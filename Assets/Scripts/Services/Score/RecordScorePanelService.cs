using Zenject;

public class RecordScorePanelService: Iservice
{
    private IScoreDataManager _scoreDataManager;
    private RecordScorePanelView _recordView;
    private int _recordScore;
    private IFabric _fabric;

    public void ActivateService()
    {
        
    }

    [Inject]
    public void Constructor(
        IFabric fabric,
        IScoreDataManager scoreDataManager)
    {
        _recordView = fabric.SpawnObjectAndGetType<RecordScorePanelView>();
        _scoreDataManager = scoreDataManager;
        _scoreDataManager.RecordChanged += UpdateView;
        _recordScore = _scoreDataManager.GetRecordScore();
        UpdateView(_recordScore);
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

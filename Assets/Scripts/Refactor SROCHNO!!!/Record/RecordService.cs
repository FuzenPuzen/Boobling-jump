using Zenject;

public class RecordService
{
    private RecordSLDataService _recordSLData = new();
    private RecordPanelView _recordView;

    [Inject]
    public void Constructor(IFabric fabric)
    {
        _recordView = fabric.SpawnObjectAndGetType<RecordPanelView>();
        _recordView.SetRecordData(_recordSLData);
    }

    public int GetRecord()
    {
        return _recordSLData.GetRecord();
    }

    public void SetRecord(int _score)
    {
        if (_recordSLData.GetRecord() < _score)
        {
            _recordSLData.SetNewRecord(_score);
        }
    }

    public void HideView()
    {
        _recordView.HideView();
    }

}

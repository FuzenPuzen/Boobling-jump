using System.Reflection.Emit;
using Zenject;

public class RecordService
{
    private RecordSLData _recordSLData = new();
    private RecordView _recordView;

    [Inject]
    public void Constructor(IFabric fabric)
    {
        _recordView = fabric.SpawnObjectAndGetType<RecordView>();
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

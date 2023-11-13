using Newtonsoft.Json;
using UnityEngine;
using Zenject;

public class RecordSLDataService
{
    const string RecordKey = "RecordKey";
    private Loader _loader;

    private int _recordScore;

    public RecordSLDataService()
    {
        if (!PlayerPrefs.HasKey(RecordKey))
        {
            SaveRecord(CreateRecord());
        }
        else
        {
            LoadRecord();
        }
    }

    [Inject]
    public void Constructor(Loader SLDataService)
    {
        _loader = SLDataService;
    }

    private int CreateRecord()
    {
        return _recordScore = 0;
    }

    private void LoadRecord()
    {
        _recordScore = _loader.LoadItem<int>(RecordKey);
    }

    private void SaveRecord(int score)
    {
        _recordScore = score;
        _loader.SaveItem<int>(score, RecordKey);
    }

    public int GetRecord() { return _recordScore; }
    public void SetNewRecord(int score) { SaveRecord(score); }
}

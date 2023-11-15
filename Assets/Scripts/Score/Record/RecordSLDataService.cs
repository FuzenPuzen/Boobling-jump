using MLALib;
using UnityEngine;

public class RecordSLDataService
{
    const string RecordKey = "RecordKey";
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

    private int CreateRecord()
    {
        return _recordScore = 0;
    }

    private void LoadRecord()
    {
        _recordScore = SaveLoader.LoadItem<int>(RecordKey);
    }

    private void SaveRecord(int score)
    {
        _recordScore = score;
        SaveLoader.SaveItem<int>(score, RecordKey);
    }

    public int GetRecord() { return _recordScore; }
    public void SetNewRecord(int score) { SaveRecord(score); }
}

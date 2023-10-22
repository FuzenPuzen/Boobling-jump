using Newtonsoft.Json;
using UnityEngine;

public class RecordSLService
{
    const string RecordKey = "RecordKey";

    [SerializeField] private int _recordScore;
    public RecordSLService()
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
        string json = PlayerPrefs.GetString(RecordKey, "");
        _recordScore = JsonConvert.DeserializeObject<int>(json);
    }

    private void SaveRecord(int score)
    {
        string json = JsonConvert.SerializeObject(score);
        PlayerPrefs.SetString(RecordKey, json);
        PlayerPrefs.Save();
    }

    public int GetRecord() { return _recordScore; }
    public void SetNewRecord(int score) { SaveRecord(score); }
}

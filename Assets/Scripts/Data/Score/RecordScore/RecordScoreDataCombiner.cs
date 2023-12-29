using MLALib;
using System;
using Zenject;

public class RecordScoreDataCombiner
{
    IRecordScoreStorageData _recordScoreStoradeData;
    private const string _recordSLDataKey = "RecordKey";
    RecordScoreSLData _recordScoreSLData;

    [Inject]
    public void Constructor(IRecordScoreStorageData recordScoreStoradeData)
    {
        _recordScoreStoradeData = recordScoreStoradeData;
        _recordScoreStoradeData.RecordDataSlChanged += SaveRecordScoreData;
        GetAndPullSimpleDataToStorage();
    }

    private void GetAndPullSimpleDataToStorage()
    {
        _recordScoreSLData = new();
        _recordScoreSLData = SaveLoader.LoadData<RecordScoreSLData>(_recordScoreSLData, _recordSLDataKey);
        _recordScoreStoradeData.SetRecordScoreSLData(_recordScoreSLData);
    }

    private void SaveRecordScoreData(IRecordScoreSLData data)
    {
        SaveLoader.SaveItem<RecordScoreSLData>((RecordScoreSLData)data, _recordSLDataKey);
    }
}

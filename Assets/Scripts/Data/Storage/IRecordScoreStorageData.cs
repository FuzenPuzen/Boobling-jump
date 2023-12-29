using System;

public interface IRecordScoreStorageData
{
    public event Action<IRecordScoreSLData> RecordDataSlChanged;
    public IRecordScoreSLData GetRecordScoreSLData();
    public void SetRecordScoreSLData(IRecordScoreSLData newRecordScoreData);
}
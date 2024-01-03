using System;
using Zenject;

public class ScoreDataManager : IScoreDataManager
{
    public event Action<int> RecordChanged;
    public event Action<int> CurrentScoreChanged;

    private CurrentScoreData _currentScoreData;
    private RecordScoreSLData _recordScoreSLData;
    private ICurrentScoreStorageData _currentScoreStoradeData;
    private IRecordScoreStorageData _recordScoreStoradeData;

    [Inject]
    public void Constructor(
        ICurrentScoreStorageData currentScoreStoradeData,
        IRecordScoreStorageData recordScoreStoradeData
        )
    {
        _currentScoreStoradeData = currentScoreStoradeData;
        _recordScoreStoradeData = recordScoreStoradeData;
        _currentScoreData = (CurrentScoreData)_currentScoreStoradeData.GetCurrentScoreData();
        _recordScoreSLData = (RecordScoreSLData)_recordScoreStoradeData.GetRecordScoreSLData();
       
    }

    public int GetCurrentScore()
    {
        return _currentScoreData.Score;
    }

    public int GetRecordScore()
    {
        return _recordScoreSLData.Score;
    }

    public void AddCurrentScore(int count)
    {
        _currentScoreData.Score += count;
        _currentScoreStoradeData.SetCurrentScoreData(_currentScoreData);
        CurrentScoreChanged?.Invoke(_currentScoreData.Score);
        CheckCurrentScoreForNewRecord();
    }

    private void CheckCurrentScoreForNewRecord()
    {
        if (_currentScoreData.Score > _recordScoreSLData.Score)
        {
            _recordScoreSLData.Score = _currentScoreData.Score;
            _recordScoreStoradeData.SetRecordScoreSLData(_recordScoreSLData);
            RecordChanged?.Invoke(_recordScoreSLData.Score);
        }
    }
}

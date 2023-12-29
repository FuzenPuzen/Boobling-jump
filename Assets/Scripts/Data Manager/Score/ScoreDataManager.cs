using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScoreDataManager : IScoreDataManager
{
    public event Action<int> currentScoreMoreRecord;

    private CurrentScoreData _currentScoreData;
    private ICurrentScoreStoradeData _currentScoreStoradeData;

    [Inject]
    public void Constructor(ICurrentScoreStoradeData currentScoreStoradeData)
    {
        _currentScoreStoradeData = currentScoreStoradeData;
        _currentScoreData = (CurrentScoreData)_currentScoreStoradeData.GetCurrentScoreData();
    }

    public int GetCurrentScore()
    {
        return _currentScoreData.Score;
    }

    public int GetRecordScore()
    {
        return 0;
    }

    public void AddCurrentScore(int count)
    {
        _currentScoreData.Score += count;
        _currentScoreStoradeData.SetCurrentScoreData(_currentScoreData);
    }

    public CurrentScoreData GetCurrentScoreData()
    {
        return _currentScoreData;
    }
}

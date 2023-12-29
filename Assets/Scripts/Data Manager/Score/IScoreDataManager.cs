using System;
using System.Collections;
using UnityEngine;


public interface IScoreDataManager
{
    public event Action<int> currentScoreMoreRecord;
    public int GetCurrentScore();
    public int GetRecordScore();
    public void AddCurrentScore(int count);
    CurrentScoreData GetCurrentScoreData();
}

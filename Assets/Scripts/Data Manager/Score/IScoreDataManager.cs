using System;
using System.Collections;
using UnityEngine;


public interface IScoreDataManager
{
    public event Action<int> RecordChanged;
    public event Action<int> CurrentScoreChanged;
    public int GetCurrentScore();
    public int GetRecordScore();
    public void AddCurrentScore(int count);
}

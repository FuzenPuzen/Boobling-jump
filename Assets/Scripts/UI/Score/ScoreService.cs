using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Zenject;

public class ScoreService 
{
    private ScoreView _scoreView;
    private int[] _difficultyLevels;
    private int _difficultyLevel;
    private int _currentlevel;

    private Action _ChangeTierAction;
    private ScoreData _scoreData = new();

    [Inject]
    public void Constructor(IFabric fabric)
    {
        _scoreView = fabric.SpawnObjectAndGetType<ScoreView>();
        _scoreView.SetScoreChangedAction(ScoreChanged);
        _scoreView.SetScoreData(_scoreData);
    }

    public int GetScore()
    {
        return _scoreData.Score;
    }

    public void SetScoreCallback(Action action, int[] difficultyLevels)
    {
        _ChangeTierAction = action;
        _difficultyLevels = difficultyLevels;
        _currentlevel = difficultyLevels[0];
    }

    private void ScoreChanged()
    {
        if (_scoreData.Score >= _currentlevel)
        {
            if (_difficultyLevel < _difficultyLevels.Length - 1)
            {
                _difficultyLevel++;
                _ChangeTierAction.Invoke();
                _currentlevel = _difficultyLevels[_difficultyLevel];
            }
            else
            {
                _ChangeTierAction.Invoke();
                _currentlevel = int.MaxValue;
            }
        }
    }

    public void HideView()
    {
        _scoreView.HideView();
    }

}

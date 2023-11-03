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
    private int _score;
    private int _record;

    [Inject]
    public void Constructor(IFabric fabric)
    {
        _scoreView = fabric.SpawnObjectAndGetType<ScoreView>();
        _scoreView.SetScoreChangedAction(ScoreChanged);
    }

    public int GetScore() =>
        _score = _scoreView.GetScore();

    public void SetScoreCallback(Action action, int[] difficultyLevels)
    {
        _ChangeTierAction = action;
        _difficultyLevels = difficultyLevels;
    }

    private void ScoreChanged()
    {
        if (_score >= _currentlevel)
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

}

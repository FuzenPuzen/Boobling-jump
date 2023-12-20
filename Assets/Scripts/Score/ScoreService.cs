using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Zenject;

public class ScoreService : Iservice
{
    private ScoreView _scoreView;
    private int[] _difficultyLevels;
    private int _difficultyLevel;
    private int _currentlevel;
    private int _scoreIncreaseStep;
    private IFabric _fabric;

    private Action _ChangeTierAction;
    private ScoreData _scoreData = new();

    [Inject]
    public void Constructor(IFabric fabric)
    {
        _fabric = fabric;       
    }

    public void ActivateService()
    {
        _scoreView = _fabric.SpawnObjectAndGetType<ScoreView>();
        _scoreView.SetActionOnScoreChange(OnScoreChange);
        _scoreView.SetScoreData(_scoreData);
    }

    public int GetScore()
    {
        return _scoreData.Score;
    }

    public void SetActionOnTierChange(Action action, int[] difficultyLevels, int scoreIncreaseStep)
    {
        _scoreIncreaseStep = scoreIncreaseStep;
        _ChangeTierAction = action;
        _difficultyLevels = difficultyLevels;
        _currentlevel = difficultyLevels[0];
    }

    private void OnScoreChange()
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
                _currentlevel += _scoreIncreaseStep;
            }
        }
    }

    public void HideView()
    {
        _scoreView.HideView();
    }

}

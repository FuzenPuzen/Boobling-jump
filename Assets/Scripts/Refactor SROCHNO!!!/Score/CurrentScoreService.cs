using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Zenject;

public class CurrentScoreService : Iservice
{
    private CurrentScorePanelView _currentScoreView;
    private int[] _difficultyLevels;
    private int _difficultyLevel;
    private int _currentlevel;
    private int _scoreIncreaseStep;
    private IFabric _fabric;
    private IScoreDataManager _scoreDataManager;

    private Action _ChangeTierAction;
    private CurrentScoreData _currentScoreData = new();

    [Inject]
    public void Constructor(IFabric fabric, IScoreDataManager scoreDataManager)
    {
        _fabric = fabric;    
        _scoreDataManager = scoreDataManager;
    }

    public void ActivateService()
    {
        _currentScoreData = _scoreDataManager.GetCurrentScoreData();
        _currentScoreView = _fabric.SpawnObjectAndGetType<CurrentScorePanelView>();
        _currentScoreView.SetActionOnScoreChange(OnScoreChange);
        _currentScoreView.SetScoreData(_currentScoreData);
    }

    private void OnScoreChange(int count)
    {
        _scoreDataManager.AddCurrentScore(count);
       /* if (_currentScoreData.Score >= _currentlevel)
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
        }*/
    }

    public void HideView()
    {
        _currentScoreView.HideView();
    }

}

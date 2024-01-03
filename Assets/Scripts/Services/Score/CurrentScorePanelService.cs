using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Zenject;

public class CurrentScorePanelService : Iservice
{
    private CurrentScorePanelView _currentScoreView;
    private IFabric _fabric;
    private IScoreDataManager _scoreDataManager;

    private int _currentScore;

    [Inject]
    public void Constructor(IFabric fabric, IScoreDataManager scoreDataManager)
    {
        _fabric = fabric;    
        _scoreDataManager = scoreDataManager;
    }

    public void ActivateService()
    {
        _currentScore = _scoreDataManager.GetCurrentScore();
        _currentScoreView = _fabric.SpawnObjectAndGetType<CurrentScorePanelView>();
        _currentScoreView.SetActionOnScoreChange(OnScoreChange);
        _scoreDataManager.CurrentScoreChanged += UpdateView;
        UpdateView(_currentScore);
    }

    private void OnScoreChange(int count)
    {
        _scoreDataManager.AddCurrentScore(count);
    }

    public void HideView()
    {
        _currentScoreView.HideView();
    }

    private void UpdateView(int record)
    {
        _currentScoreView.UpdateView(record);
    }

}

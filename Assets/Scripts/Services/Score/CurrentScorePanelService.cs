using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Zenject;

public class CurrentScorePanelService : IService
{
    private CurrentScorePanelView _currentScoreView;
    private IViewFabric _fabric;
    private IScoreDataManager _scoreDataManager;
    private IRoomViewManager _roomViewManager;

    private int _currentScore;

    [Inject]
    public void Constructor(
        IViewFabric fabric,
        IScoreDataManager scoreDataManager,
        IRoomViewManager roomViewManager)
    {
        _fabric = fabric;    
        _scoreDataManager = scoreDataManager;
        _roomViewManager = roomViewManager;
    }

    public void ActivateService()
    {
        _currentScore = _scoreDataManager.GetCurrentScore();
        _currentScoreView = _fabric.SpawnObject<CurrentScorePanelView>(_roomViewManager.GetCurrentScorePos());
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

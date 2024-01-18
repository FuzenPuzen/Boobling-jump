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
    private IMarkerService _markerService;

    private int _currentScore;

    [Inject]
    public void Constructor(
        IViewFabric fabric,
        IScoreDataManager scoreDataManager,
        IMarkerService markerService)
    {
        _fabric = fabric;    
        _scoreDataManager = scoreDataManager;
        _markerService = markerService;
    }

    public void ActivateService()
    {
        _currentScore = _scoreDataManager.GetCurrentScore();
        Transform parent = _markerService.GetTransformMarker<CurrentScorePanelPosMarker>().transform;
        _currentScoreView = _fabric.SpawnObject<CurrentScorePanelView>(parent);
        _currentScoreView.SetActionOnScoreChange(OnScoreChange);
        _scoreDataManager.CurrentScoreChanged += UpdateView;
        UpdateView(_currentScore);
    }
    public void DeactivateScoreChange()
    {
        _currentScoreView.SetActionOnScoreChange(null);
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

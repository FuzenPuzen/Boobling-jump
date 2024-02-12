using System;
using UnityEngine;
using Zenject;

public class SectionViewService 
{
    private SectionView _sectionView;
    private Action<SectionViewService> _sectionActivatorExitAction;
    private Action _sectionActivatorEnterAction;
    private IMarkerService _markerService;
    private IPoolsViewService _poolsViewService;
    private IPoolViewService _coinPoolViewService;

    [Inject]
    public void Constructor(IMarkerService  markerService, IPoolsViewService _poolsViewService)
    {
        _coinPoolViewService = _poolsViewService.GetPool<DropedCoinViewService>();
        _markerService =  markerService;
    }

    public void SpawnDroppedCoin()
    {
        for (int i = 0; i < _sectionView.transform.childCount; i++)
        {
            if (!_sectionView.transform.GetChild(i).TryGetComponent(out IView view)) continue;
            if (!view.GetCanSpawnCoin()) continue;
            StartValues startValues = new();
            startValues.StartPos = _sectionView.transform.GetChild(i).transform.position;
            _coinPoolViewService.GetItem().ActivateService(startValues);
        }
    }

    public void SetSectionView(SectionView sectionView)
    {
        _sectionView = sectionView;
        if (_sectionView != null)
        {
            SetSectionViewActivatorExit();
            SetSectionViewActivatorEnter();
        }
    }

    public SectionView GetSectionView() => _sectionView;

    public void SetValuesToView(float movingTime, float endPosX)
    {
        _sectionView.SetValues(movingTime, endPosX);
    }

    public void ActivateSection()
    {
        _sectionView.ActivateView(_markerService.GetTransformMarker<SectionStartPosMarker>().transform.position);
        Debug.Log(_markerService.GetTransformMarker<SectionStartPosMarker>().transform.position);
    }

    //Enter Activator Collider
    public void SetSectionViewActivatorEnter()
    {
        _sectionView.SetSectionActivatorEnterAction(SectionActivatorEnter);
    }

    public void SetSectionActivatorEnterAction(Action action)
    {
        _sectionActivatorEnterAction = action;
    }

    private void SectionActivatorEnter()
    {
        _sectionActivatorEnterAction?.Invoke();
    }

    //Exit Activator Collider
    public void SetSectionViewActivatorExit()
    {
        _sectionView.SetSectionActivatorExitAction(SectionActivatorExit);
    }

    public void SetSectionActivatorExitAction(Action<SectionViewService> action)
    {
        _sectionActivatorExitAction = action;
    }

    private void SectionActivatorExit()
    {
        _sectionActivatorExitAction?.Invoke(this);
    }




}

using EventBus;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Zenject;

public interface IRepaintService : IService
{
    void RepaintAll();
}

public class RepaintService : IRepaintService
{
    private List<IRepaint> _repaints = new List<IRepaint>();
    private EventBinding<OnRepaintAwake> _painterAwake;
    private IRepaintDataManager _repaintDataManager;
    private RepaintSOData _currentRepaintSOData;

    [Inject]
    public void Constructor(IRepaintDataManager repaintDataManager)
    {
        _repaintDataManager = repaintDataManager;
        
    }

    public void ActivateService()
    {
        _painterAwake = new(SetRepaint);
    }

    public void DeActivateService()
    {
        _painterAwake.Remove(SetRepaint);
    }
    public void SetRepaint(OnRepaintAwake repaintAwake)
    {
        _currentRepaintSOData = _repaintDataManager.GetRandomRepaintSOData();
        _repaints.Add(repaintAwake.Marker);
        repaintAwake.Marker.Repaint(_currentRepaintSOData);
    }
    public void RepaintAll()
    {
        _currentRepaintSOData = _repaintDataManager.GetRandomRepaintSOData();
        foreach (IRepaint repaint in _repaints)
        {
            repaint.Repaint(_currentRepaintSOData);
        }
    }
}

public interface IRepaint
{
    public void Repaint(RepaintSOData repaintData);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StoolsService
{
    private List<IStoolService> _stoolServices = new();
    private IStoolFabric _stoolFabric;
    private TimerService _timerService;

    [Inject]
    public StoolsService(IStoolFabric stoolFabric, TimerService timerService)
    {
        _stoolFabric = stoolFabric;
        _timerService = timerService;
        _timerService.secondAction += SpawnStool;
        SpawnStool();
    }

    public void SpawnStool()
    {
        Debug.Log("SpawnStool");
        int i = Random.Range(0, 2);
        _stoolServices.Add(_stoolFabric.SpawnStool(i));      
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SectionsService
{
    private List<IStoolService> _stoolServices = new();
    private IFabric _fabric;
    private TimerService _timerService;
    private TiersService _tiersService;
    private GameObject section;
    private int stoolId = 0;

    [Inject]
    public SectionsService(IFabric fabric, TimerService timerService)
    {
        _fabric = fabric;
        _timerService = timerService;
        _timerService.secondAction += SpawnStool;
        _tiersService = new(_fabric.SpawnObjectAndGetType<TiersView>(new Vector3(0,0,30)));
        SetNewSection();
    }

    public void SetNewSection()
    {
        section = _tiersService.GetSectionFromTier(0);
        
    }

    public void SpawnStool()
    {
        if (stoolId >= section.transform.childCount)
        {
            SetNewSection();
            stoolId = 0;
            SpawnStool();
            return;
        }
        var stool = section.transform.GetChild(stoolId);
        stool.transform.position = new(-10, 1.6f, 0);
        if (stool.GetComponent<IStoolView>() != null)
        {
            StoolService stoolService = new StoolService(stool.GetComponent<IStoolView>());
            _stoolServices.Add(stoolService);
            stoolService.SetViewCompleteInstruction(RemoveFromList);
        }
        stoolId++;
    }

    public void RemoveFromList(StoolService stoolService)
    {
        _stoolServices.Remove(stoolService);
    }

}

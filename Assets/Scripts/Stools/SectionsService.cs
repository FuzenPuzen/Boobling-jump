using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SectionsService
{
    private List<IStoolService> _stoolServices = new();
    private IFabric _fabric;
    private ITimerService _timerService;
    private TiersService _tiersService;
    private ScoreService _scoreService;
    private GameObject section;
    private int stoolId = 0;
    private int[] _difficultyLevels = {500,700,1000,1300};
    private int _tierId = 0;


    [Inject]
    public SectionsService(IFabric fabric, ITimerService timerService, ScoreService scoreService)
    {
        _scoreService = scoreService;
        _scoreService.SetScoreCallback(AddTier, _difficultyLevels);
        _fabric = fabric;
        _timerService = timerService;
        _timerService.SetRepeatActionOnView(1.5f, TakeStool);
        _tiersService = new(_fabric.SpawnObjectAndGetType<TiersView>(new Vector3(0,0,30)));
        SetNewSection();
    }

    public void AddTier()
    {        
        _tierId++;
    }

    public void SetNewSection()
    {
        //section = _tiersService.GetSectionFromTier(_tierId);
        section = _tiersService.GetSectionFromTier(3);
        
    }

    public void TakeStool()
    {
        if (stoolId >= section.transform.childCount)
        {
            SetNewSection();
            stoolId = 0;
            TakeStool();
            return;
        }
        var stool = section.transform.GetChild(stoolId);
        stool.transform.position = new(-10, 1.6f, 0);
        if (stool.GetComponent<IStoolView>() != null)
        {
            StoolService stoolService = new StoolService(stool.GetComponent<IStoolView>());
            _stoolServices.Add(stoolService);
            stoolService.SetViewCompleteInstruction(RemoveStoolFromList);
        }
        stoolId++;
    }

    public void RemoveStoolFromList(StoolService stoolService)
    {
        _stoolServices.Remove(stoolService);
    }

}

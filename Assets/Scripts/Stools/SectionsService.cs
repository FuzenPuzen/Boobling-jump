using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class SectionsService : Iservice
{
    private List<IStoolService> _stoolServices = new();
    private IFabric _fabric;
    private ITimerService _timerService;
    private TiersService _tiersService;
    private ScoreService _scoreService;
    private GameObject section;
    private int stoolId = 0;
    private int[] _difficultyLevels = 
    {
        6,
        14,
        23,
        30,
        38,
        46,
        54,
        62,
        78,
        85,
    };
    private int _tierId = 0;
    private int _changeSpeedScore = 200;
    private float _stoolSpawnTime = 1.5f;
    private float _minStoolSpawnTime = 1f;

    public void ActivateService()
    {
        _scoreService.SetScoreCallback(AddTier, _difficultyLevels, _changeSpeedScore);
        _timerService.SetActionOnView(_stoolSpawnTime, TakeStool);
        _tiersService = new(_fabric.SpawnObjectAndGetType<TiersView>(new Vector3(0, 0, 30)));
        SetNewSection();
    }

    [Inject]
    public void Constructor(IFabric fabric, ITimerService timerService, ScoreService scoreService)
    {
        _scoreService = scoreService;
        _fabric = fabric;
        _timerService = timerService;
    }

    public void AddTier()
    {
        if (_tierId < _difficultyLevels.Count())
        {
            _tierId++;
            return;
        }
        if (_stoolSpawnTime > _minStoolSpawnTime)
        {
            _stoolSpawnTime -= 0.1f;
        }

    }

    public void SetNewSection()
    {
        section = _tiersService.GetSectionFromTier(_tierId);        
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
            stoolService.ActivateService();
            _stoolServices.Add(stoolService);
            stoolService.SetViewCompleteInstruction(RemoveStoolFromList);
        }
        stoolId++;
        _timerService.SetActionOnView(_stoolSpawnTime, TakeStool);
    }

    public void RemoveStoolFromList(StoolService stoolService)
    {
        _stoolServices.Remove(stoolService);
    }

}

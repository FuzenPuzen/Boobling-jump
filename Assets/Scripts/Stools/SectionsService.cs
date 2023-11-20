using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SectionsService : Iservice
{
    private List<IStoolService> _stoolServices = new();
    private IFabric _fabric;
    private ITimerService _timerService;
    private TiersService _tiersService;
    private ScoreService _scoreService;
    private ConfigSO _configSO;

    private GameObject section;
    private int stoolId = 0;
    
    private int _tierId = 0;
    private int _changeSpeedScore = 200;
    private float _stoolSpawnTime = 1.5f;
    private float _minStoolSpawnTime = 1f;

    public void ActivateService()
    {
        _scoreService.SetActionOnTierChange(AddTier, _configSO._difficultyLevels, _changeSpeedScore);
        _timerService.SetActionOnTimerComplete(_stoolSpawnTime, TakeStool);
        _tiersService = new(_fabric.SpawnObjectAndGetType<TiersView>(new Vector3(0, 0, 30)));
        SetNewSection();
    }

    [Inject]
    public void Constructor(IFabric fabric, ITimerService timerService, ScoreService scoreService, ConfigSO configSO)
    {
        _scoreService = scoreService;
        _fabric = fabric;
        _timerService = timerService;
        _configSO = configSO;
    }

    private void AddTier()
    {
        if (_tierId < _configSO._difficultyLevels.Count())
        {
            _tierId++;
            return;
        }
        if (_stoolSpawnTime > _minStoolSpawnTime)
        {
            _stoolSpawnTime -= 0.1f;
        }

    }

    private void SetNewSection()
    {
        section = _tiersService.GetSectionFromTier(_tierId);        
    }

    private void TakeStool()
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
            StoolService stoolService = new(stool.GetComponent<IStoolView>());
            stoolService.ActivateService();
            _stoolServices.Add(stoolService);
            stoolService.SetActionOnMoveComplete(RemoveStoolFromList);
        }
        stoolId++;
        _timerService.SetActionOnTimerComplete(_stoolSpawnTime, TakeStool);
    }

    public void RemoveStoolFromList(StoolService stoolService)
    {
        _stoolServices.Remove(stoolService);
    }

}

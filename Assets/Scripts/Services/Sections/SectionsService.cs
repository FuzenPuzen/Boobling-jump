using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SectionsService : Iservice
{
    private List<IStoolService> _stoolServices = new();
    private IFabric _fabric;
    private ITimerService _timerService;
    private StoolPoolService _poolsService;
    private CurrentScoreService _scoreService;
    private ConfigSO _configSO;

    private GameObject section;
    private int stoolId = 0;
    
    private int _tierId = 0;
    private int _changeSpeedScore = 200;
    private float _stoolSpawnTime = 1.5f;
    private float _minStoolSpawnTime = 1f;
    private Vector3 _stoolStartPos = new(4.5f, 0.77f, 0);
    private Vector3 _stoolPoolPos = new(0, 0, 30);

    public void ActivateService()
    {
        //_scoreService.SetActionOnTierChange(AddTier, _configSO._difficultyLevels, _changeSpeedScore);
        _timerService.SetActionOnTimerComplete(_stoolSpawnTime, TakeStool);
        _poolsService = new(_fabric.SpawnObjectAndGetType<StoolPoolView>(_stoolPoolPos));
        SetNewSection();
    }

    [Inject]
    public void Constructor(IFabric fabric, ITimerService timerService, CurrentScoreService scoreService, ConfigSO configSO)
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
        section = _poolsService.GetSectionFromTier(_tierId);
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
        stool.transform.position = _stoolStartPos;
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

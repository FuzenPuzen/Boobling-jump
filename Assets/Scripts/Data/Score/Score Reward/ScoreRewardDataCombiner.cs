using MLALib;
using UnityEngine;
using Zenject;

public class ScoreRewardDataCombiner 
{
    private const string RemaindScoreKey = "RemaindScoreKey";
    private ScoreRewardDataPackage _scoreRewardDataPackage;
    private ISOStorageService _sOStorageService;
    private ScoreRewardSOData _scoreRewardSOData;
    private ISessionTypeDataManager _sessionTypeDataManager;

    [Inject]
    public void Constructor(ISOStorageService sOStorageService, ISessionTypeDataManager sessionTypeDataManager)
    {
        _sessionTypeDataManager = sessionTypeDataManager;
        _sOStorageService = sOStorageService;
        FillPackage();
    }

    public void FillPackage()
    {
        _scoreRewardDataPackage = new();
        if (_sessionTypeDataManager.GetTutorialGameType())
            _scoreRewardSOData = (TutorialScoreRewardSOData)_sOStorageService.GetSOByType<TutorialScoreRewardSOData>();
        else
            _scoreRewardSOData = (InfinityScoreRewardSOData)_sOStorageService.GetSOByType<InfinityScoreRewardSOData>();

        _scoreRewardDataPackage.RemaindScore = LoadRemaindScore();
        _scoreRewardDataPackage.CurrentTotalScore = LoadRemaindScore();
        _scoreRewardDataPackage.RewardScore = _scoreRewardSOData.RewardScore;
        _scoreRewardDataPackage.RewardCount = _scoreRewardSOData.RewardCount;
    }

    public ScoreRewardDataPackage GetScoreRewardDataPackage() => _scoreRewardDataPackage;

    public void SetScoreRewardDataPackage(ScoreRewardDataPackage scoreRewardDataPackage)
    {
        _scoreRewardDataPackage = scoreRewardDataPackage;
        SaveLoader.SaveItem<int>(_scoreRewardDataPackage.RemaindScore, RemaindScoreKey);
    }

    public int LoadRemaindScore()
    {
        return SaveLoader.LoadData<int>(_scoreRewardDataPackage.RemaindScore, RemaindScoreKey);
    }
}

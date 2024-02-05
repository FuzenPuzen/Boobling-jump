using System;
using UnityEngine;
using Zenject;

public class ScoreDataManager : IScoreDataManager
{
    public event Action<int> RecordChanged;
    public event Action<int> CurrentScoreChanged;
    public event Action<int> GiftScoreAchived;

    private CurrentScoreData _currentScoreData;
    private RecordScoreSLData _recordScoreSLData;
    private GiftScoreSOData _giftScoreSOData;
    private ICurrentScoreStorageData _currentScoreStoradeData;
    private IRecordScoreStorageData _recordScoreStoradeData;
    private IGiftScoreStorageData _giftScoreStorageData;

    private ScoreRewardDataPackage _scoreRewardDataPackage;
    private ScoreRewardDataCombiner _scoreRewardDataCombiner;

    private int _giftCounter = 1;

    [Inject]
    public void Constructor(
        ICurrentScoreStorageData currentScoreStoradeData,
        IRecordScoreStorageData recordScoreStoradeData,
        IGiftScoreStorageData giftScoreStorageData,
        ScoreRewardDataCombiner scoreRewardDataCombiner
        )
    {
        _scoreRewardDataCombiner = scoreRewardDataCombiner;
        _currentScoreStoradeData = currentScoreStoradeData;
        _recordScoreStoradeData = recordScoreStoradeData;
        _giftScoreStorageData = giftScoreStorageData;
        _currentScoreData = (CurrentScoreData)_currentScoreStoradeData.GetCurrentScoreData();
        _recordScoreSLData = (RecordScoreSLData)_recordScoreStoradeData.GetRecordScoreSLData();
        _giftScoreSOData = (GiftScoreSOData)_giftScoreStorageData.GetGiftScoreData();
        _scoreRewardDataPackage = _scoreRewardDataCombiner.GetScoreRewardDataPackage();
    }

    public int GetCurrentScore()
    {
        return _currentScoreData.Score;
    }

    public int GetRecordScore()
    {
        return _recordScoreSLData.Score;
    }

    public int GetGiftScore()
    {
        return _giftScoreSOData.GiftScoreSize * _giftCounter;
    }

    public void AddCurrentScore(int count)
    {
        _currentScoreData.Score += count;
        _currentScoreStoradeData.SetCurrentScoreData(_currentScoreData);
        CurrentScoreChanged?.Invoke(_currentScoreData.Score);
        CheckCurrentScoreForNewRecord();
        CheckCurrentScoreForGiftAchived();
    }

    private void CheckCurrentScoreForNewRecord()
    {
        if (_currentScoreData.Score > _recordScoreSLData.Score)
        {
            _recordScoreSLData.Score = _currentScoreData.Score;
            _recordScoreStoradeData.SetRecordScoreSLData(_recordScoreSLData);
            RecordChanged?.Invoke(_recordScoreSLData.Score);
        }
    }

    private void CheckCurrentScoreForGiftAchived()
    {
        if (_currentScoreData.Score > _giftScoreSOData.GiftScoreSize * _giftCounter)
        {
            _giftCounter++;
            GiftScoreAchived?.Invoke(_giftScoreSOData.GiftScoreSize * _giftCounter);
        }
    }

    public void OnPlayerDie()
    {
        _scoreRewardDataPackage.CurrentTotalScore += _currentScoreData.Score;
        _scoreRewardDataPackage.RewardMultiplier = _scoreRewardDataPackage.CurrentTotalScore / _scoreRewardDataPackage.RewardScore;
        if (_scoreRewardDataPackage.RewardMultiplier >= 1)
        {
            _scoreRewardDataPackage.RemaindScore = _scoreRewardDataPackage.CurrentTotalScore % _scoreRewardDataPackage.RewardScore;
        }
        _scoreRewardDataPackage.RemaindScore = _scoreRewardDataPackage.CurrentTotalScore;
        _scoreRewardDataCombiner.SetScoreRewardDataPackage(_scoreRewardDataPackage);
        MonoBehaviour.print(_scoreRewardDataPackage.RemaindScore);
    }

    public ScoreRewardDataPackage GetScoreRewardDataPackage() => _scoreRewardDataPackage; 

}

public class ScoreRewardDataPackage
{
    public int RewardScore;
    public int RewardCount;
    public int CurrentTotalScore;
    public int RemaindScore;
    public int RewardMultiplier;

    public ScoreRewardDataPackage()
    {
        RemaindScore = 0;
    }
}

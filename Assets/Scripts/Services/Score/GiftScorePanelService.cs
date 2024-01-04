using System.Collections;
using UnityEngine;
using Zenject;

public class GiftScorePanelService : IService
{
    private IFabric _fabric;
    private IScoreDataManager _scoreDataManager;
    private IRoomViewManager _roomViewManager;
    private GiftScorePanelView _giftScoreView;
    private int _giftScore;

    [Inject]
    public void Constructor(
      IFabric fabric,
      IScoreDataManager scoreDataManager, IRoomViewManager roomViewManager)
    {
        _fabric = fabric;
        _scoreDataManager = scoreDataManager;
        _roomViewManager = roomViewManager;
    }

    public void ActivateService()
    {
        _giftScoreView = _fabric.SpawnObjectAndGetType<GiftScorePanelView>(_roomViewManager.GetGiftScorePos());
        _scoreDataManager.GiftScoreAchived += UpdateView;
        _giftScore = _scoreDataManager.GetGiftScore();
        UpdateView(_giftScore);

    }


    public void HideView()
    {
        _giftScoreView.HideView();
    }

    private void UpdateView(int record)
    {
        _giftScoreView.UpdateView(record);
    }
}

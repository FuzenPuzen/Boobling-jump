using UnityEditor;
using UnityEngine;
using Zenject;

public class GiftService: IService
{
    private IScoreDataManager _scoreDataManger;
    private IFabric _fabric;
    private IRoomViewManager _roomViewManager;
    private GiftBoxViewService _giftBoxViewService;

    [Inject]
    public void Constructor(IScoreDataManager scoreDataManager, IFabric fabric, IRoomViewManager roomViewManager)
    {
        _scoreDataManger = scoreDataManager;
        _fabric = fabric;
        _roomViewManager = roomViewManager;
    }

    public void ActivateService()
    {
        _scoreDataManger.GiftScoreAchived += SpawnGift;
    }

    private void SpawnGift(int count)
    {
        GiftBoxView giftBoxView = _fabric.SpawnObjectAndGetType<GiftBoxView>(_roomViewManager.GetGiftBoxSpawnPos());
        _giftBoxViewService = new GiftBoxViewService();
        _giftBoxViewService.ActivateService(giftBoxView);
    }
}
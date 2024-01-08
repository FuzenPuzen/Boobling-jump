using UnityEditor;
using UnityEngine;
using Zenject;

public class GiftService: IService
{
    private IScoreDataManager _scoreDataManger;
    private IViewFabric _fabric;
    private IMarkerService _markerService;
    private GiftBoxViewService _giftBoxViewService;

    [Inject]
    public void Constructor(IScoreDataManager scoreDataManager, IViewFabric fabric, IMarkerService markerService)
    {
        _scoreDataManger = scoreDataManager;
        _fabric = fabric;
        _markerService = markerService;
    }

    public void ActivateService()
    {
        _scoreDataManger.GiftScoreAchived += SpawnGift;
    }

    private void SpawnGift(int count)
    {
        GiftBoxView giftBoxView = _fabric.SpawnObject<GiftBoxView>(_markerService.GetTransformMarker<GiftBoxSpawnPosMarker>().transform.position);
        _giftBoxViewService = new GiftBoxViewService();
        _giftBoxViewService.ActivateService(giftBoxView);
    }
}
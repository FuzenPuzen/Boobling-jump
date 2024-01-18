using UnityEditor;
using UnityEngine;
using Zenject;

public class GiftService: IService
{
    private IScoreDataManager _scoreDataManger;
    private IViewFabric _viewFabric;
    private IMarkerService _markerService;
    private IServiceFabric _serviceFabric;
    private GiftBoxViewService _giftBoxViewService;

    [Inject]
    public void Constructor(
        IScoreDataManager scoreDataManager,
        IViewFabric viewFabric,
        IMarkerService markerService,
        IServiceFabric serviceFabric)
    {
        _scoreDataManger = scoreDataManager;
        _viewFabric = viewFabric;
        _markerService = markerService;
        _serviceFabric = serviceFabric;
    }

    public void ActivateService()
    {
        _scoreDataManger.GiftScoreAchived += SpawnGift;
    }

    private void SpawnGift(int count)
    {
        //Заменить создание на взятие из пула
        GiftBoxView giftBoxView = _viewFabric.Init<GiftBoxView>(_markerService.GetTransformMarker<GiftBoxSpawnPosMarker>().transform.position);
        _giftBoxViewService = _serviceFabric.Init<GiftBoxViewService>();
        _giftBoxViewService.ActivateService(giftBoxView);
    }
}
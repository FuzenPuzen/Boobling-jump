using System.Collections;
using UnityEngine;
using Zenject;

public class GiftScorePanelService : IService
{
    private IViewFabric _fabric;
    private IScoreDataManager _scoreDataManager;
    private IMarkerService _markerService;
    private GiftScorePanelView _giftScoreView;
    private int _giftScore;

    [Inject]
    public void Constructor(
      IViewFabric fabric,
      IScoreDataManager scoreDataManager, IMarkerService  markerService)
    {
        _fabric = fabric;
        _scoreDataManager = scoreDataManager;
        _markerService =  markerService;
    }

    public void ActivateService()
    {
        _giftScoreView = _fabric.SpawnObject<GiftScorePanelView>(_markerService.GetTransformMarker<GiftScorePanelPosMarker>().transform.position);
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

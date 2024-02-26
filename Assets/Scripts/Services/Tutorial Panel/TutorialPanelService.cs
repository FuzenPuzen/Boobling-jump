using UnityEngine;
using Zenject;

public class TutorialPanelService : IService
{

    private TutorialPanelView _tutorialView;
    private IViewFabric _fabric;
    private IMarkerService _markerService;

    [Inject]
    public void constructor(IViewFabric fabric, IMarkerService markerService)
    {
        _fabric = fabric;
        _markerService = markerService;
    }

    public void ActivateService()
    {
        Transform parent = _markerService.GetTransformMarker<TutorialTextMarker>().transform;
        _tutorialView = _fabric.Init<TutorialPanelView>(parent);
    }

    public void DeActivateService()
    {
        MonoBehaviour.Destroy(_tutorialView.gameObject);
    }
}

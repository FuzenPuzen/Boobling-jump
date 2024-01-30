using UnityEngine;
using Zenject;

public class TutorialSectionPoolViewService : SectionPoolViewService
{
    private ISessionTypeDataManager _sessionTypeDataManager;
    [Inject]
    public void Constructor(IViewFabric fabric, ISessionTypeDataManager sessionTypeDataManager)
    {
        _fabric = fabric;
        _sessionTypeDataManager = sessionTypeDataManager;
        SpawnView();
    }


    public override void SpawnView()
    {
        _poolView = _fabric.Init<TutorialStoolPoolView>(_poolPos);
    }
}

using UnityEngine;
using Zenject;

public class TutorialPanelService : Iservice
{

    private TutorialPanelView _tutorialView;
    private IFabric _fabric;

    [Inject]
    public void constructor(IFabric fabric)
    {
        _fabric = fabric;
    }

    public void ActivateService()
    {
        _tutorialView = _fabric.SpawnObjectAndGetType<TutorialPanelView>(new(0.9f, 1.54f, 3.96f));
        _tutorialView.transform.rotation = Quaternion.Euler(-76, 0, 0);
    }

    public void DeActivateService()
    {
        MonoBehaviour.Destroy(_tutorialView.gameObject);
    }
}

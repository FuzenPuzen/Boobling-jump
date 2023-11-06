using UnityEngine;
using Zenject;

public class TutorialService : Iservice
{

    private TutorialView _tutorialView;

    [Inject]
    public void constructor(IFabric fabric)
    {
        _tutorialView = fabric.SpawnObjectAndGetType<TutorialView>(new(0.9f, 1.54f, 3.96f));
        _tutorialView.transform.rotation = Quaternion.Euler(-76, 0, 0);
    }

    public void ActivateService()
    {
        _tutorialView.StartDestoroyTimer();

    }
}

using Zenject;

public class SectionSimpleJumpBehaviour : SectionBehaviour
{
    private ISessionTypeDataManager _sessionTypeDataManager;
    private IPoolViewService _infinityStoolPoolViewService;
    private IPoolViewService _tutorialStoolPoolViewService;

    [Inject]
    public void Constructor(ISessionTypeDataManager sessionTypeDataManager,
                            InfinityStoolPoolViewService infinityStoolPoolViewService,
                            TutorialStoolPoolViewService tutorialStoolPoolViewService)
    {
        _tutorialStoolPoolViewService = tutorialStoolPoolViewService;
        _infinityStoolPoolViewService = infinityStoolPoolViewService;
        _sessionTypeDataManager = sessionTypeDataManager;
        SetPool(_sessionTypeDataManager.GetTutorialGameType());
    }

    public void SetPool(bool tutorial)
    {
        if (tutorial)
        {
            _poolViewService = _tutorialStoolPoolViewService;
        }
        else
        {
            _poolViewService = _infinityStoolPoolViewService;
        }

    }
}

using Zenject;

public class SectionSimpleJumpBehaviour : SectionBehaviour
{
    private ISessionTypeDataManager _sessionTypeDataManager;
    private ISectionPoolViewService _infinityStoolPoolViewService;
    private ISectionPoolViewService _tutorialStoolPoolViewService;

    [Inject]
    public void Constructor(ISessionTypeDataManager sessionTypeDataManager,
                            InfinitySectionPoolViewService infinityStoolPoolViewService,
                            TutorialSectionPoolViewService tutorialStoolPoolViewService)
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

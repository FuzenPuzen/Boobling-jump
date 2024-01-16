using Zenject;

public class SectionRollBehaviour : SectionBehaviour
{
    [Inject]
    public void Constructor(RollSectionPoolViewService poolViewService)
    {
        _poolViewService = poolViewService;
    }
}

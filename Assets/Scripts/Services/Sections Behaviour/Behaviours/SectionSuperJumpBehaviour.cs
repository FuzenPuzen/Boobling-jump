using Zenject;

public class SectionSuperJumpBehaviour : SectionBehaviour
{
    [Inject]
    public void Constructor(SuperJumpSectionPoolViewService poolViewService)
    {
        _poolViewService = poolViewService;
    }
}

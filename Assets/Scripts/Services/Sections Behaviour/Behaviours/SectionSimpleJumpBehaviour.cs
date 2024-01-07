using Zenject;

public class SectionSimpleJumpBehaviour : SectionBehaviour
{
    [Inject]
    public void Constructor(InfinityStoolPoolViewService poolViewService)
    {
        _poolViewService = poolViewService;
    }
}

using Zenject;

public class SectionSuperJumpBehaviour : SectionBehaviour
{
    [Inject]
    public void Constructor(SuperJumpStoolPoolViewService poolViewService)
    {
        _poolViewService = poolViewService;
    }
}

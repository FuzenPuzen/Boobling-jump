using Zenject;

public class SectionRollBehaviour : SectionBehaviour
{
    [Inject]
    public void Constructor(RollStoolPoolViewService poolViewService)
    {
        _poolViewService = poolViewService;
    }
}

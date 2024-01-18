public class InfinitySectionPoolViewService : SectionPoolViewService
{
    public override void SpawnView()
    {
        _poolView = _fabric.Init<InfinityStoolPoolView>(_poolPos);
    }
}

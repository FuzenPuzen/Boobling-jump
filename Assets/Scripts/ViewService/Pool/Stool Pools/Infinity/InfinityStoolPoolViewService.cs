public class InfinityStoolPoolViewService : SectionPoolViewService
{
    public override void SpawnView()
    {
        _poolView = _fabric.SpawnObject<InfinityStoolPoolView>(_poolPos);
    }
}

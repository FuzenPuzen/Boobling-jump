public class InfinityStoolPoolViewService : PoolViewService
{
    public override void SpawnView()
    {
        _poolView = _fabric.SpawnObject<InfinityStoolPoolView>(_poolPos);
    }
}

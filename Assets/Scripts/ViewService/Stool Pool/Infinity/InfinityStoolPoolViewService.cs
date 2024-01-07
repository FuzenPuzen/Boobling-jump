public class InfinityStoolPoolViewService : PoolViewService
{
    public override void SpawnView()
    {
        _poolView = _fabric.SpawnObjectAndGetType<InfinityStoolPoolView>(_stoolPoolPos);
    }
}

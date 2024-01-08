public class RollStoolPoolViewService : PoolViewService
{
    public override void SpawnView()
    {
        _poolView = _fabric.SpawnObjectAndGetType<RollStoolPoolView>(_stoolPoolPos);
    }
}

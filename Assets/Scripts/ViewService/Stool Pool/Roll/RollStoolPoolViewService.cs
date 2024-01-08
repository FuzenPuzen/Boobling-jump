public class RollStoolPoolViewService : PoolViewService
{
    public override void SpawnView()
    {
        _poolView = _fabric.SpawnObject<RollStoolPoolView>(_stoolPoolPos);
    }
}

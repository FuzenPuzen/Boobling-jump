public class SuperJumpStoolPoolViewService : PoolViewService
{
    public override void SpawnView()
    {
        _poolView = _fabric.SpawnObjectAndGetType<SuperJumpStoolPoolView>(_stoolPoolPos);
    }
}

public class SuperJumpStoolPoolViewService : PoolViewService
{
    public override void SpawnView()
    {
        _poolView = _fabric.SpawnObject<SuperJumpStoolPoolView>(_poolPos);
    }
}

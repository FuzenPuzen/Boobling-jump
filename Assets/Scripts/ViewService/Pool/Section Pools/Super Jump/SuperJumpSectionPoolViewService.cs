public class SuperJumpSectionPoolViewService : SectionPoolViewService
{
    public override void SpawnView()
    {
        _poolView = _fabric.SpawnObject<SuperJumpStoolPoolView>(_poolPos);
    }
}

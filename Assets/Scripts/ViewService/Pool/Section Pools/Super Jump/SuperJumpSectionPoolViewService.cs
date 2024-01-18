public class SuperJumpSectionPoolViewService : SectionPoolViewService
{
    public override void SpawnView()
    {
        _poolView = _fabric.Init<SuperJumpStoolPoolView>(_poolPos);
    }
}

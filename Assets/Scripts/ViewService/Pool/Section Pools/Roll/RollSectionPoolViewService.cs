public class RollSectionPoolViewService : SectionPoolViewService
{
    public override void SpawnView()
    {
        _poolView = _fabric.Init<RollStoolPoolView>(_poolPos);
    }
}

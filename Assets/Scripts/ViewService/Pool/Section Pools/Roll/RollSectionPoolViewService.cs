public class RollSectionPoolViewService : SectionPoolViewService
{
    public override void SpawnView()
    {
        _poolView = _fabric.SpawnObject<RollStoolPoolView>(_poolPos);
    }
}

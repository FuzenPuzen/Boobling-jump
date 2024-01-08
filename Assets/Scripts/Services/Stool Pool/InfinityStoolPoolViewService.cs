using UnityEngine;
using Zenject;

public class InfinityStoolPoolViewService : IPoolViewService
{
    private InfinityStoolPoolView _poolView;
    private IViewFabric _fabric;
    private Vector3 _stoolPoolPos = new(0, 0, 30);


    [Inject]
    public void Constructor(IViewFabric fabric)
    {
        _fabric = fabric;
        SpawnView();
    }

    public void SpawnView()
    {
        _poolView = _fabric.SpawnObject<InfinityStoolPoolView>(_stoolPoolPos);
    }

    public SectionView GetSection()
    {
        return _poolView.GetSection();
    }

    public void ReturnSection(SectionView section)
    {
        _poolView.ReturnSection(section);
    }
}

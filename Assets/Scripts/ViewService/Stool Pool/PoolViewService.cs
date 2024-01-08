using UnityEngine;
using Zenject;

public abstract class PoolViewService : IPoolViewService
{
    private protected IstoolPoolView _poolView;
    private protected IFabric _fabric;
    private protected Vector3 _stoolPoolPos = new(0, 0, 30);

    [Inject]
    public void Constructor(IFabric fabric)
    {
        _fabric = fabric;
        SpawnView();
    }

    public abstract void SpawnView();

    public SectionView GetSection() => _poolView.GetSection();

    public void ReturnSection(SectionView section) => _poolView.ReturnSection(section);
}

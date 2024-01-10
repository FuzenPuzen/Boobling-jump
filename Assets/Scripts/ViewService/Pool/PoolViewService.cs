using UnityEngine;
using Zenject;

public abstract class PoolViewService : IPoolViewService
{
    private protected IPoolView _poolView;
    private protected IViewFabric _fabric;
    private protected Vector3 _poolPos = new(0, 0, 30);

    [Inject]
    public void Constructor(IViewFabric fabric)
    {
        _fabric = fabric;
        SpawnView();
    }

    public abstract void SpawnView();

    public SectionView GetSection() => _poolView.GetSection();

    public void ReturnSection(SectionView section) => _poolView.ReturnSection(section);
}

using UnityEngine;
using Zenject;

public class TutorialStoolPoolViewService : PoolViewService
{
    public override void SpawnView()
    {
        _poolView = _fabric.SpawnObject<TutorialStoolPoolView>(_poolPos);
    }
}

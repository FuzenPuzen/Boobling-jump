using UnityEngine;
using Zenject;

public class TutorialSectionPoolViewService : SectionPoolViewService
{
    public override void SpawnView()
    {
        _poolView = _fabric.SpawnObject<TutorialStoolPoolView>(_poolPos);
    }
}

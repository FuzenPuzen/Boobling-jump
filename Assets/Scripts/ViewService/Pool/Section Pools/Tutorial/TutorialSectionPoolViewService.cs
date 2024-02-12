using UnityEngine;
using Zenject;

public class TutorialSectionPoolViewService : SectionPoolViewService
{

    public override void SpawnView()
    {
        _poolView = _fabric.Init<TutorialStoolPoolView>(_poolPos);
    }
}

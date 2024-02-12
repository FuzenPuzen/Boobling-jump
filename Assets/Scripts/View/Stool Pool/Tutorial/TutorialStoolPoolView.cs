using EventBus;
using UnityEngine;

public class TutorialStoolPoolView : StoolPoolView
{
    private int _currectSection = 0;
    private SectionView _section;

    public override SectionView GetSection()
    {
        if (_currectSection >= transform.childCount) _currectSection = 0;
        _section = transform.GetChild(_currectSection).GetComponent<SectionView>();
        _section.gameObject.SetActive(true);
        MonoBehaviour.print(_currectSection);
        //EventBus<OnTutorialMaxSection>.Raise(new() { MaxSection = _currectSection });
        _currectSection++;
        return _section;
    }
}

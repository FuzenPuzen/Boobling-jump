public class TutorialStoolPoolView : StoolPoolView
{
    private int _currectSection = 0;
    private SectionView _section;

    public override SectionView GetSection()
    {
        _section = transform.GetChild(_currectSection++).GetComponent<SectionView>();
        _section.gameObject.SetActive(true);
        return _section;
    }
}

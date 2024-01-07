public class TutorialStoolPoolView : StoolPoolView
{
    private int _currectSection = 0;

    public override SectionView GetSection() => transform.GetChild(_currectSection++).GetComponent<SectionView>();
}

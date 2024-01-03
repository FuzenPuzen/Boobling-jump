public class SectionViewService 
{
    private SectionView _sectionView;

    public void SetSectionView(SectionView sectionView)
    {
        _sectionView = sectionView;
    }

    public void SetValuesToView(float movingTime, float endPosX)
    {
        _sectionView.SetValues(movingTime, endPosX);
    }

    public void ActivateView()
    {
        _sectionView.ActivateView();
    }
    
}

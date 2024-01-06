using System;

public class SectionViewService 
{
    private SectionView _sectionView;
    private Action<SectionViewService> _sectionActivatorExitAction;
    private Action _sectionActivatorEnterAction;

    public void SetSectionView(SectionView sectionView)
    {
        _sectionView = sectionView;
        if (_sectionView != null)
        {
            SetSectionViewActivatorExit();
            SetSectionViewActivatorEnter();
        }
    }

    public SectionView GetSectionView() => _sectionView;

    public void SetValuesToView(float movingTime, float endPosX)
    {
        _sectionView.SetValues(movingTime, endPosX);
    }

    public void ActivateSection()
    {
        _sectionView.ActivateView();
    }

    //Enter Activator Collider
    public void SetSectionViewActivatorEnter()
    {
        _sectionView.SetSectionActivatorEnterAction(SectionActivatorEnter);
    }

    public void SetSectionActivatorEnterAction(Action action)
    {
        _sectionActivatorEnterAction = action;
    }

    private void SectionActivatorEnter()
    {
        _sectionActivatorEnterAction?.Invoke();
    }

    //Exit Activator Collider
    public void SetSectionViewActivatorExit()
    {
        _sectionView.SetSectionActivatorExitAction(SectionActivatorExit);
    }

    public void SetSectionActivatorExitAction(Action<SectionViewService> action)
    {
        _sectionActivatorExitAction = action;
    }

    private void SectionActivatorExit()
    {
        _sectionActivatorExitAction?.Invoke(this);
    }




}

using System.Collections.Generic;

public class SectionBehaviour : ISectionBehaviour
{
    private protected IPoolViewService _poolViewService;
    private protected List<SectionViewService> _sectionViewServices = new();
    private protected SectionViewService _currentSectionViewService;

    public virtual void StartBehaviour()
    {
        GetAndStartNewSection();
    }

    private protected virtual void SetSectionActivatorEnterAction(SectionViewService sectionViewService)
    {
        sectionViewService.SetSectionActivatorEnterAction(GetAndStartNewSection);
    }
    private protected virtual void SetSectionActivatorExitAction(SectionViewService sectionViewService)
    {
        sectionViewService.SetSectionActivatorExitAction(ReturnSectionToPool);
    }

    private protected virtual void GetAndStartNewSection()
    {
        _currentSectionViewService = new SectionViewService();
        _sectionViewServices.Add(_currentSectionViewService);
        _currentSectionViewService.SetSectionView(_poolViewService.GetSection());
        SetSectionActivatorEnterAction(_currentSectionViewService);
        SetSectionActivatorExitAction(_currentSectionViewService);
        _currentSectionViewService.ActivateSection();

    }

    private protected virtual void ReturnSectionToPool(SectionViewService sectionViewService)
    {
        _sectionViewServices.Remove(sectionViewService);
        _poolViewService.ReturnSection(sectionViewService.GetSectionView());
    }

    public virtual void StopBehaviour()
    {
        foreach (SectionViewService section in _sectionViewServices)
            _poolViewService.ReturnSection(section.GetSectionView());
        _sectionViewServices.Clear();
    }

    public virtual void UpdateBehaviour()
    {
        throw new System.NotImplementedException();
    }
}

using Zenject;
using System.Collections.Generic;

public class SectionSimpleJumpBehaviour : ISectionBehavior
{
    private IPoolViewService _poolViewService;
    private List<SectionViewService> _sectionViewServices = new();
    private SectionViewService _currentSectionViewService;

    [Inject]
    public void Constructor(InfinityStoolPoolViewService poolViewService)
    {
        _poolViewService = poolViewService;
    }

    public void StartBehavior()
    {
        GetAndStartNewSection();
    }

    private void SetSectionActivatorEnterAction(SectionViewService sectionViewService)
    {
        sectionViewService.SetSectionActivatorEnterAction(GetAndStartNewSection);
    }
    private void SetSectionActivatorExitAction(SectionViewService sectionViewService)
    {
        sectionViewService.SetSectionActivatorExitAction(ReturnSectionToPool);
    }

    private void GetAndStartNewSection()
    {
        _currentSectionViewService = new SectionViewService();
        _sectionViewServices.Add(_currentSectionViewService);
        _currentSectionViewService.SetSectionView(_poolViewService.GetSection());
        SetSectionActivatorEnterAction(_currentSectionViewService);
        SetSectionActivatorExitAction(_currentSectionViewService);
        _currentSectionViewService.ActivateSection();
        
    }

    private void ReturnSectionToPool(SectionViewService sectionViewService)
    {
        _sectionViewServices.Remove(sectionViewService);
        _poolViewService.ReturnSection(sectionViewService.GetSectionView());
    }

    public void StopBehavior()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateBehavior()
    {
        throw new System.NotImplementedException();
    }
}

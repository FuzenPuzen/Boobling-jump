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
        _currentSectionViewService = new SectionViewService();
        _sectionViewServices.Add(_currentSectionViewService);
        _currentSectionViewService.SetSectionView(_poolViewService.GetSection());
        _currentSectionViewService.ActivateView();
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

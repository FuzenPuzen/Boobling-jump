using Zenject;

public class SectionRollBehaviour : SectionBehaviour
{
    [Inject]
    public void Constructor(RollSectionPoolViewService poolViewService)
    {
        _poolViewService = poolViewService;
    }

    private protected override void GetAndStartNewSection()
    {
        _currentSectionViewService = _serviceFabric.InitMultiple<SectionViewService>();
        _sectionViewServices.Add(_currentSectionViewService);
        _currentSectionViewService.SetSectionView(_poolViewService.GetSection());
        _currentSectionViewService.SetValuesToView(5f, -60f);
        SetSectionActivatorEnterAction(_currentSectionViewService);
        SetSectionActivatorExitAction(_currentSectionViewService);
        _currentSectionViewService.ActivateSection();       
    }
}

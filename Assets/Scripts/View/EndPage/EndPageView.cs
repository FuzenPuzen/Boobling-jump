using Zenject;
using UnityEngine;

public class EndPageView : MonoBehaviour
{

}

public class EndPageViewService : IService
{
	private IViewFabric _viewFabric;
	private IServiceFabric _serviceFabric;
	private EndPageView _EndPageView;
    private IMarkerService _markerService;
	private GetBonusPanelViewService _bonusPanelViewService;
	private EndChoosePanelViewService _endChoosePanelViewService;
	
	[Inject]
	public void Constructor(IViewFabric viewFabric, IMarkerService markerService, IServiceFabric serviceFabric)
	{
		_markerService = markerService;
        _viewFabric = viewFabric;
		_serviceFabric = serviceFabric;
	}

	public void ActivateService()
	{       
        _EndPageView = _viewFabric.SpawnObject<EndPageView>();
		_bonusPanelViewService = _serviceFabric.Create<GetBonusPanelViewService>();
		_bonusPanelViewService.ActivateService();
        _endChoosePanelViewService = _serviceFabric.Create<EndChoosePanelViewService>();
		_endChoosePanelViewService.ActivateService();
	}
}

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
        _EndPageView = _viewFabric.Init<EndPageView>();
		_bonusPanelViewService = _serviceFabric.Init<GetBonusPanelViewService>();
		_bonusPanelViewService.ActivateService();
        _endChoosePanelViewService = _serviceFabric.Init<EndChoosePanelViewService>();
		_endChoosePanelViewService.ActivateService();
		_bonusPanelViewService.SetOnGetBonusCompleteAction(OnGetBonusComplete);
		_bonusPanelViewService.ShowView();
	}

	public void OnGetBonusComplete()
	{
		_bonusPanelViewService.HideView();
		_endChoosePanelViewService.ShowView();
	}
}

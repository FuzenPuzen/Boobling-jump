using Zenject;
using UnityEngine;

public class TutorialEndPageView : MonoBehaviour
{

}

public class TutorialEndPageViewService : IService
{
	private IViewFabric _fabric;
	private IServiceFabric _serviceFabric;
	private TutorialEndPageView _TutorialEndPageView;
    private IMarkerService _markerService;
	private TutorialEndPanelViewService _TutorialEndPanelViewService;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService, IServiceFabric serviceFabric)
	{
        _serviceFabric = serviceFabric;
        _markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _TutorialEndPageView = _fabric.Init<TutorialEndPageView>();
        _TutorialEndPanelViewService = _serviceFabric.InitSingle<TutorialEndPanelViewService>();
        _TutorialEndPanelViewService.ActivateService();

    }
}

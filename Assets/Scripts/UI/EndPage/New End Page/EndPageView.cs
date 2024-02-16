using Zenject;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndPageView : MonoBehaviour
{

}

public class EndPageViewService : IService
{
	private IViewFabric _fabric;
	private IServiceFabric _serviceFabric;
	private EndPageView _EndPageView;
    private IMarkerService _markerService;
    private ISessionTypeDataManager _sessionTypeDataManager;


    [Inject]
	public void Constructor(IViewFabric viewFabric, IMarkerService markerService,
							IServiceFabric serviceFabric, ISessionTypeDataManager sessionTypeDataManager)
	{
        _sessionTypeDataManager = sessionTypeDataManager;
        _markerService = markerService;
        _fabric = viewFabric;
		_serviceFabric = serviceFabric;
	}

	public void ActivateService()
	{       
        _EndPageView = _fabric.Init<EndPageView>();
        bool isTutorial = _sessionTypeDataManager.GetTutorialGameType();
        if (isTutorial)
            _serviceFabric.InitSingle<TutorialCirclePanelViewService>().ActivateService();
        else
            _serviceFabric.InitSingle<FilledCirclePanelViewService>().ActivateService();
        _serviceFabric.InitSingle<ButtonsPanelViewService>().ActivateService();
    }

}

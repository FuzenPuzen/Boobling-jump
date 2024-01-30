using Zenject;
using UnityEngine;

public class TutorialEndPageView : MonoBehaviour
{

}

public class TutorialEndPageViewService : IService
{
	private IViewFabric _fabric;
	private TutorialEndPageView _TutorialEndPageView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _TutorialEndPageView = _fabric.Init<TutorialEndPageView>();
	}
}

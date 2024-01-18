using Zenject;
using UnityEngine;

public class BackButtonCanvasView : MonoBehaviour
{
    internal void HideView()
    {
        gameObject.SetActive(false);
    }

    internal void ShowView()
    {
        gameObject.SetActive(true);
    }
}

public class BackButtonCanvasViewService : IService
{
	private IViewFabric _fabric;
    private IServiceFabric _serviceFabric;
    private BackButtonCanvasView _BackButtonCanvasView;
    private BackButtonViewService _BackButtonViewService;
    private IMarkerService _markerService;

	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService, IServiceFabric serviceFabric)
	{
        _serviceFabric = serviceFabric;
        _markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _BackButtonCanvasView = _fabric.Init<BackButtonCanvasView>();
        _BackButtonViewService = _serviceFabric.Init<BackButtonViewService>();
        _BackButtonViewService.ActivateService();
        HideView();
    }
    internal void HideView()
    {
        _BackButtonCanvasView.HideView();
    }

    internal void ShowView()
    {
        _BackButtonCanvasView.ShowView();
    }

}

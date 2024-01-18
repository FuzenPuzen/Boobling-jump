using Zenject;
using UnityEngine;

public class EndChoosePanelView : MonoBehaviour
{
    public void Awake()
    {
        gameObject.SetActive(false);
    }
    public void ActivateView()
    {

    }

    public void ShowView()
    {
        gameObject.SetActive(true);
    }

    public void HideView()
    {
        gameObject.SetActive(false);
    }
}

public class EndChoosePanelViewService : IService
{
	private IViewFabric _fabric;
	private EndChoosePanelView _endChoosePanelView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
        Transform parent = _markerService.GetTransformMarker<EndPageMarker>().transform;
        _endChoosePanelView = _fabric.SpawnObject<EndChoosePanelView>(parent);
        _endChoosePanelView.ActivateView();

    }
    public void ShowView()
    {
        _endChoosePanelView.ShowView();

    }
    public void HideView()
    {
        _endChoosePanelView.HideView();

    }
}

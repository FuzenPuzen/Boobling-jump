using Zenject;
using UnityEngine;
using EventBus;
using UnityEngine.UI;

public class MenuInfinityPanelView : MonoBehaviour
{
    private Button _gameButton;

    private void Start()
    {
        _gameButton = GetComponent<Button>();
        _gameButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        EventBus<OnClickGame>.Raise();
    }
}

public class MenuInfinityPanelViewService : IService
{
	private IViewFabric _fabric;
	private MenuInfinityPanelView _MenuInfinityPanelView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _MenuInfinityPanelView = _fabric.Init<MenuInfinityPanelView>(_markerService.GetTransformMarker<MenuMainPageMarker>().transform);
	}
}

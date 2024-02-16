using Zenject;
using UnityEngine;
using EventBus;
using UnityEngine.UI;

public class SettingButtonView : MonoBehaviour
{
    private Button _menuButton;

    private void Start()
    {
        _menuButton = GetComponent<Button>();
        _menuButton.onClick.AddListener(SettingsOpen);
    }

    public void SettingsOpen()
    {
        EventBus<OnSettingOpen>.Raise();
    }

    internal void HideView()
    {
        gameObject.SetActive(false);
    }

    internal void ShowView()
    {
        gameObject.SetActive(true);
    }
}

public class SettingButtonViewService : IService
{
	private IViewFabric _fabric;
	private SettingButtonView _SettingButtonView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
        Transform parent = _markerService.GetTransformMarker<NavigationCanvasMarker>().transform;
        _SettingButtonView = _fabric.Init<SettingButtonView>(parent);
	}

    public void HideView()
    {
        _SettingButtonView.HideView();
    }

    public void ShowView()
    {
        _SettingButtonView.ShowView();
    }
}

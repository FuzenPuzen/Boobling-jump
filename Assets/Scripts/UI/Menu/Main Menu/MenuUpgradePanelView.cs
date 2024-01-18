using Zenject;
using UnityEngine;
using UnityEngine.UI;
using EventBus;

public class MenuUpgradePanelView : MonoBehaviour
{
    private Button _upgradeButton;

    private void Start()
    {
        _upgradeButton = GetComponent<Button>();
        _upgradeButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        EventBus<OnClickUpgrade>.Raise();
    }
}

public class MenuUpgradePanelViewService : IService
{
	private IViewFabric _fabric;
    private MenuUpgradePanelView _menuUpgradePanelView;
    private IMarkerService _markerService;

    [Inject]
    public void Constructor(IViewFabric fabric, IMarkerService markerService)
    {
        _markerService = markerService;
        _fabric = fabric;
    }

    public void ActivateService()
    {
        _menuUpgradePanelView = _fabric.Init<MenuUpgradePanelView>(_markerService.GetTransformMarker<MenuMainPageMarker>().transform);
    }
}

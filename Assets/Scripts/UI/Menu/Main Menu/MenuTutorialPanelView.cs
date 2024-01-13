using Zenject;
using UnityEngine;
using UnityEngine.UI;
using EventBus;
using EventBus.Example.MyGame.Events;

public class MenuTutorialPanelView : MonoBehaviour
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

public class MenuTutorialPanelViewService : IService
{
    private IViewFabric _fabric;
    private MenuTutorialPanelView _menuTutorialShopPanelView;
    private IMarkerService _markerService;

    [Inject]
    public void Constructor(IViewFabric fabric, IMarkerService markerService)
    {
        _markerService = markerService;
        _fabric = fabric;
    }

    public void ActivateService()
    {
        _menuTutorialShopPanelView = _fabric.SpawnObject<MenuTutorialPanelView>(_markerService.GetTransformMarker<MenuMainPageMarker>().transform);
    }
}
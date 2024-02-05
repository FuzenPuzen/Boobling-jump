using Zenject;
using UnityEngine;
using UnityEngine.UI;
using EventBus;

public class MenuSkinShopPanelView : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        EventBus<OnClickSkinShop>.Raise();
    }

    public void UpdateView()
    {

    }
}

public class MenuSkinShopPanelViewService : IService
{
    private IViewFabric _fabric;
    private MenuSkinShopPanelView _menuSkinShopPanelView;
    private PlayerSkinView _playerSkinView;
    private PlayerSkinData _currentPlayerSkinData;
    private IMarkerService _markerService;
    private IPlayerSkinDataManager _playerSkinDataManager;

    [Inject]
    public void Constructor(IViewFabric fabric, IMarkerService markerService,
                            IPlayerSkinDataManager playerSkinDataManager)
    {
        _playerSkinDataManager = playerSkinDataManager;
        _markerService = markerService;
        _fabric = fabric;
    }

    public void ActivateService()
    {
        Transform parent = _markerService.GetTransformMarker<MenuMainPageMarker>().transform;
        _menuSkinShopPanelView = _fabric.Init<MenuSkinShopPanelView>(parent);
        _currentPlayerSkinData = _playerSkinDataManager.GetCurrentSkin();

        Transform parentofSkin = _menuSkinShopPanelView.transform;
        _currentPlayerSkinData = _playerSkinDataManager.GetCurrentSkin();
        GameObject playerSkin = _currentPlayerSkinData.PlayerSkinSOData.SkinPrefab;
        _playerSkinView = _fabric.Init<PlayerSkinView>(playerSkin, new(0, 0, -200), parentofSkin);
    }

    public void SpawnSkin(PlayerSkinData playerSkinData)
    {
        if (_currentPlayerSkinData == playerSkinData)
            return;
        _playerSkinView?.gameObject.SetActive(false);

        Transform parent = _menuSkinShopPanelView.transform;
        _currentPlayerSkinData = _playerSkinDataManager.GetCurrentSkin();
        GameObject playerSkin = _currentPlayerSkinData.PlayerSkinSOData.SkinPrefab;        
        _playerSkinView = _fabric.Init<PlayerSkinView>(playerSkin, new(0, 0, -200), parent);
    }

    public void UpdateView()
    {
        SpawnSkin(_playerSkinDataManager.GetCurrentSkin());
    }
}

using Zenject;
using UnityEngine;
using UnityEngine.UI;
using EventBus;
using System;
using TMPro;

public class MenuUpgradePanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentJumpLevelText;
    [SerializeField] private TMP_Text _currentJumpDurationText;

    [SerializeField] private TMP_Text _currentRollLevelText;
    [SerializeField] private TMP_Text _currentRollDurationText;

    [SerializeField] private Button _upgradeButton;
    public Action OnViewEnable;


    public void OnEnable()
    {
        OnViewEnable?.Invoke();
    }

    private void Start()
    {
        _upgradeButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        EventBus<OnClickUpgrade>.Raise();
    }

    internal void UpdateView(UpgradeDataPackage upgradeJumpPackage, UpgradeDataPackage upgradeRollPackage)
    {
        _currentJumpLevelText.text = upgradeJumpPackage.currentLevel.ToString() + "\n ”р";
        _currentJumpDurationText.text = upgradeJumpPackage.currentDuration.ToString() + "\n сек";

        _currentRollLevelText.text = upgradeRollPackage.currentLevel.ToString() + "\n ”р";
        _currentRollDurationText.text = upgradeRollPackage.currentDuration.ToString() + "\n сек";
    }
}

public class MenuUpgradePanelViewService : IService
{
	private IViewFabric _fabric;
    private MenuUpgradePanelView _menuUpgradePanelView;
    private IPlayerBehaviourDataManager _playerBehaviourDataManager;
    private IMarkerService _markerService;
    private UpgradeDataPackage _upgradeJumpPackage;   
    private UpgradeDataPackage _upgradeRollPackage;   

    [Inject]
    public void Constructor(IViewFabric fabric, IMarkerService markerService, IPlayerBehaviourDataManager playerBehaviourDataManager)
    {
        _playerBehaviourDataManager = playerBehaviourDataManager;
        _markerService = markerService;
        _fabric = fabric;
    }

    public void UpdateView()
    {
        _upgradeJumpPackage = _playerBehaviourDataManager.GetUpgradeSuperJumpDataPackage();
        _upgradeRollPackage = _playerBehaviourDataManager.GetUpgradeRollDataPackage();
        _menuUpgradePanelView.UpdateView(_upgradeJumpPackage, _upgradeRollPackage);
    }

    public void ActivateService()
    {
        _menuUpgradePanelView = _fabric.Init<MenuUpgradePanelView>(_markerService.GetTransformMarker<MenuMainPageMarker>().transform);
        _menuUpgradePanelView.OnViewEnable = UpdateView;
        UpdateView();
    }
}

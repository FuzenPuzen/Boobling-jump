using Zenject;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RollUpgradePanelView : MonoBehaviour
{
	[SerializeField] private TMP_Text _currentLevelText;
    [SerializeField] private TMP_Text _nextLevelText;
    [SerializeField] private TMP_Text _currentDurationText;
    [SerializeField] private TMP_Text _nextDurationText;
    [SerializeField] private TMP_Text _updateCostText;
	[SerializeField] private Button _updateButton;
	public Action buyUpgradeAction;

    public void Start()
    {
		_updateButton.onClick.AddListener(BuyUpgrade);
    }

	private void BuyUpgrade()
	{
        buyUpgradeAction?.Invoke();
    }

    public void UpdateView(UpgradeDataPackage upgradeDataPackage)
    {
		_currentLevelText.text = "Уровень: " + upgradeDataPackage.currentLevel.ToString();
		_currentDurationText.text = "Длительность: " + upgradeDataPackage.currentDuration.ToString();
		if (upgradeDataPackage.isLastLevel)
		{
			_updateCostText.text = "Прокачано";
            _updateButton.onClick.RemoveAllListeners();
            _nextLevelText.text = " ";
            _nextDurationText.text = " ";
			return;
        }
        _updateCostText.text = "Стоимоть: " + upgradeDataPackage.nextLevelCost.ToString();
        _nextLevelText.text = "некст ур: " + ++upgradeDataPackage.currentLevel;
        _nextDurationText.text = "некст длительность: " + upgradeDataPackage.nextDuration.ToString();
    }
}

public class RollUpgradePanelViewService : IService
{
	private IViewFabric _fabric;
	private RollUpgradePanelView _rollUpgradePanelView;
	private IPlayerBehaviourDataManager _playerBehaviourDataManager;
    private IMarkerService _markerService;
    private UpgradeDataPackage _upgradeDataPackage;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService, IPlayerBehaviourDataManager playerBehaviourDataManager)
	{
        _playerBehaviourDataManager = playerBehaviourDataManager;
        _markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
        Transform parent = _markerService.GetTransformMarker<UpgradePageMarker>().transform;
        _rollUpgradePanelView = _fabric.Init<RollUpgradePanelView>(parent);
		_rollUpgradePanelView.buyUpgradeAction = BuyUpgrade;
        
        UpdateView();
    }

	public void UpdateView()
	{
        _upgradeDataPackage = _playerBehaviourDataManager.GetUpgradeRollDataPackage();
        _rollUpgradePanelView.UpdateView(_upgradeDataPackage);
    }

    private void BuyUpgrade()
    {
        if (_playerBehaviourDataManager.BuyRollLevel(_upgradeDataPackage.nextLevelCost))
        {
            UpdateView();
        }
    }
}

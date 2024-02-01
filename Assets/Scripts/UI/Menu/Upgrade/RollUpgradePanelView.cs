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
    [SerializeField] private Image _currentLevelProgress;
    [SerializeField] private Image _NextLevelProgress;

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
        float filledLevel = (float)upgradeDataPackage.currentLevel * 0.1f;
        _currentLevelProgress.fillAmount = filledLevel;
        _NextLevelProgress.fillAmount = filledLevel + 0.1f;

        _currentLevelText.text = upgradeDataPackage.currentLevel.ToString() + "\n ��";
		_currentDurationText.text = upgradeDataPackage.currentDuration.ToString() + "\n ���";
		if (upgradeDataPackage.isLastLevel)
		{
			_updateCostText.text = "���������";
            _updateButton.onClick.RemoveAllListeners();
            _nextLevelText.text = " ";
            _nextDurationText.text = " ";
			return;
        }
        _updateCostText.text =  upgradeDataPackage.nextLevelCost.ToString() ;
        _nextLevelText.text = ++upgradeDataPackage.currentLevel + "\n ��";
        _nextDurationText.text = upgradeDataPackage.nextDuration.ToString() + "\n ���";
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

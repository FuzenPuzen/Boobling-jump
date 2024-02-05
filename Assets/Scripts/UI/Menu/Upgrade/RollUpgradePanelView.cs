using Zenject;
using UnityEngine;

public class RollUpgradePanelView : UpgradePanelView
{

}

public class RollUpgradePanelViewService : IService
{
    private const string skillName = "роллинг";
	private IViewFabric _fabric;
	private UpgradePanelView _rollUpgradePanelView;
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
        _rollUpgradePanelView.SetName(skillName);
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

using Zenject;
using UnityEngine;

public class SuperJumpUpgradePanelView : UpgradePanelView
{
    
}

public class SuperJumpUpgradePanelViewService : IService
{
    private const string skillName = "супер прыжок";
    private IViewFabric _fabric;
	private UpgradePanelView _superJumpUpgradePanelView;
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
        _superJumpUpgradePanelView = _fabric.Init<SuperJumpUpgradePanelView>(parent);
        _superJumpUpgradePanelView.transform.SetAsFirstSibling();
        _superJumpUpgradePanelView.BuyUpgradeAction = BuyUpgrade;
        _superJumpUpgradePanelView.SetName(skillName);
        UpdateView();
    }

	public void UpdateView()
	{
        _upgradeDataPackage = _playerBehaviourDataManager.GetUpgradeSuperJumpDataPackage();
        _superJumpUpgradePanelView.UpdateView(_upgradeDataPackage);
    }

    private void BuyUpgrade()
    {
        if (_playerBehaviourDataManager.BuySuperJumpLevel(_upgradeDataPackage.nextLevelCost))
        {
            UpdateView();
            _superJumpUpgradePanelView.UpgradeAnim();
        }
    }
}

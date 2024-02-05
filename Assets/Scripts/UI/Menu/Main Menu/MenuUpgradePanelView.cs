using Zenject;
using UnityEngine;
using UnityEngine.UI;
using EventBus;
using System;
using TMPro;
using System.Collections.Generic;

public class MenuUpgradePanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentJumpLevelText;
    [SerializeField] private TMP_Text _currentJumpDurationText;

    [SerializeField] private TMP_Text _currentRollLevelText;
    [SerializeField] private TMP_Text _currentRollDurationText;

    [SerializeField] private List<Image> _jumpLevelProgresses;
    [SerializeField] private List<Image> _jumpLevelIcons;

    [SerializeField] private List<Image> _rollLevelProgresses;
    [SerializeField] private List<Image> _rollLevelIcons;

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

    public static void CalculateFilledProgress(List<Image> imageList, List<Image> iconsList, int level)
    {
        if (level == 0) return;
        int mod = level % 10;

        if (mod == 0)
        {
            imageList[level / 10 - 1].fillAmount = 1;
            iconsList[level / 10 - 1].gameObject.SetActive(true);
            return;
        }

        if (mod == 1 && level / 10 >= 1)
        {
            imageList[level / 10 - 1].fillAmount = 0;            
        }

        int filledColorId = level / 10;
        float filledAmount = (float)mod / 10;
        iconsList[level / 10].gameObject.SetActive(true);
        imageList[filledColorId].fillAmount = filledAmount;
    }

    internal void UpdateView(UpgradeDataPackage upgradeJumpPackage, UpgradeDataPackage upgradeRollPackage)
    {
        CalculateFilledProgress(_jumpLevelProgresses, _jumpLevelIcons, upgradeJumpPackage.currentLevel);
        CalculateFilledProgress(_rollLevelProgresses, _rollLevelIcons, upgradeRollPackage.currentLevel);

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

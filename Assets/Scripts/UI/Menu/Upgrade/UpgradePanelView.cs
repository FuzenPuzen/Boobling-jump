using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentLevelText;
    [SerializeField] private TMP_Text _nextLevelText;
    [SerializeField] private TMP_Text _currentDurationText;
    [SerializeField] private TMP_Text _nextDurationText;
    [SerializeField] private TMP_Text _updateCostText;
    [SerializeField] private Button _updateButton;

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _nextLevelNameText;

    [SerializeField] private List<Image> _currentLevelProgresses;
    [SerializeField] private List<Image> _currentLevelIcons;

    [SerializeField] private List<Image> _nextLevelProgresses;
    [SerializeField] private List<Image> _nextLevelIcons;

    [SerializeField] private GameObject _nextLevelZone;

    public Action buyUpgradeAction;

    public void Start()
    {
        _updateButton.onClick.AddListener(BuyUpgrade);
    }

    private void BuyUpgrade()
    {
        buyUpgradeAction?.Invoke();
    }

    public void SetName(string name)
    {
        _nameText.text = name;
        _nextLevelNameText.text = name;
    }

    public void CalculateFilledProgress(List<Image> imageList, List<Image> iconsList, int level)
    {
        if (level == 0)
        {
            iconsList[level / 10].gameObject.SetActive(true);
            return;
        }
        int mod = level % 10;

        if (mod == 0)
        {
            imageList[level / 10 - 1].fillAmount = 1;
            return;
        }

        if (mod == 1 && level > 1)
        {
            imageList[level / 10 - 1].fillAmount = 0;
            iconsList[level / 10 - 1].gameObject.SetActive(false);
        }
        iconsList[level / 10].gameObject.SetActive(true);
        int filledColorId = level / 10;
        float filledAmount = (float)mod / 10;
        imageList[filledColorId].fillAmount = filledAmount;
    }

    public void UpdateView(UpgradeDataPackage upgradeDataPackage)
    {
        CalculateFilledProgress(_currentLevelProgresses, _currentLevelIcons, upgradeDataPackage.currentLevel);

        _currentLevelText.text = upgradeDataPackage.currentLevel.ToString() + "\n Ур";
        _currentDurationText.text = upgradeDataPackage.currentDuration.ToString() + "\n сек";
        if (upgradeDataPackage.isLastLevel)
        {
            _updateCostText.text = "Прокачано";
            _updateButton.onClick.RemoveAllListeners();
            _nextLevelText.text = " ";
            _nextDurationText.text = " ";
            _nextLevelZone.SetActive(false);
            return;
        }
        CalculateFilledProgress(_nextLevelProgresses, _nextLevelIcons, upgradeDataPackage.currentLevel + 1);
        _updateCostText.text = upgradeDataPackage.nextLevelCost.ToString();
        _nextLevelText.text = ++upgradeDataPackage.currentLevel + "\n Ур";
        _nextDurationText.text = upgradeDataPackage.nextDuration.ToString() + "\n сек";
    }
}

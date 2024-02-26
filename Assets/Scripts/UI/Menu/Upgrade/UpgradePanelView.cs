using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    [SerializeField] private GridLayoutGroup _levelsGreed;

    [SerializeField] private GameObject _nextLevelZone;

    private Sequence _upgradeAnim;
    private float _upgradeAnimTime = 0.25f;

    public Action BuyUpgradeAction;

    public void Start()
    {
        _updateButton.onClick.AddListener(BuyUpgrade);
    }

    private void BuyUpgrade()
    {
        BuyUpgradeAction?.Invoke();
    }

    public void SetName(string name)
    {
        _nameText.text = name;
        _nextLevelNameText.text = name;
    }

    public void CalculateFilledProgress(List<Image> imageList, List<Image> iconsList, UpgradeDataPackage upgradeDataPackage)
    {
        int level = upgradeDataPackage.currentLevel;

        if(upgradeDataPackage.isLastLevel)
            iconsList[level / 10 - 1].gameObject.SetActive(true);

        if (level == 0)
        {
            iconsList[level / 10].gameObject.SetActive(true);
            return;
        }
        int mod = level % 10;

        if (mod == 0)
        {
            imageList[level / 10 - 1].fillAmount = 1;
            iconsList[level / 10 - 1].gameObject.SetActive(true);
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
        CalculateFilledProgress(_currentLevelProgresses, _currentLevelIcons, upgradeDataPackage);

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

        ++upgradeDataPackage.currentLevel;
        CalculateFilledProgress(_nextLevelProgresses, _nextLevelIcons, upgradeDataPackage);
        _updateCostText.text = upgradeDataPackage.nextLevelCost.ToString();
        _nextLevelText.text = upgradeDataPackage.currentLevel + "\n Ур";
        _nextDurationText.text = upgradeDataPackage.nextDuration.ToString() + "\n сек";
    }

    public void UpgradeAnim()
    {
        _upgradeAnim = DOTween.Sequence();
        //_upgradeAnim.Append
        _upgradeAnim.Append(DOTween.To(() => _levelsGreed.spacing.y, x => _levelsGreed.spacing = new Vector2(0, x), 35, _upgradeAnimTime / 2));
        _upgradeAnim.Append(DOTween.To(() => _levelsGreed.spacing.y, x => _levelsGreed.spacing = new Vector2(0, x), 120, _upgradeAnimTime / 2));
    }

    private void OnDisable()
    {
        _upgradeAnim.Complete();
        _upgradeAnim.Kill();
    }
}

using Zenject;

public interface IPlayerBehaviourDataManager : IService
{
	public bool BuySuperJumpLevel(int coins);
	public bool BuyRollLevel(int coins);
    public UpgradeDataPackage GetUpgradeSuperJumpDataPackage();
    public UpgradeDataPackage GetUpgradeRollDataPackage();

    public float GetSuperJumpCurrentDuration();

    public float GetRollCurrentDuration();
}

public class PlayerBehaviourDataManager : IPlayerBehaviourDataManager
{
    private PlayerRollBehaviourSLData _playerRollBehaviourSLData;
    private PlayerRollBehaviourSODatas _playerRollBehaviourSODatas;

    private PlayerSuperJumpBehaviourSODatas _playerSuperJumpBehaviourSODatas;
    private PlayerSuperJumpBehaviourSLData _playerSuperJumpBehaviourSLData;

	private PlayerBehaviourDataCombiner _playerBehaviourDataCombiner;
	private ICoinDataManager _coinDataManager;

    [Inject]
	public void Constructor(PlayerBehaviourDataCombiner playerBehaviourDataCombiner,
							ICoinDataManager coinDataManager,
							IPlayerBehaviourStorageData playerBehaviourStorageData)
	{
		_coinDataManager = coinDataManager;
		_playerBehaviourDataCombiner = playerBehaviourDataCombiner;
    }
	
	public void ActivateService()
	{
        _playerSuperJumpBehaviourSLData = _playerBehaviourDataCombiner.playerSuperJumpBehaviourSLData;
        _playerRollBehaviourSLData = _playerBehaviourDataCombiner.playerRollBehaviourSLData;
        _playerSuperJumpBehaviourSODatas = _playerBehaviourDataCombiner.GetPlayerSuperJumpBehaviourSODatas();
        _playerRollBehaviourSODatas = _playerBehaviourDataCombiner.GetPlayerRollBehaviourSODatas();
    }

    public float GetSuperJumpCurrentDuration()
    {
        return _playerSuperJumpBehaviourSODatas.dictionary[_playerSuperJumpBehaviourSLData.level].duration;
    }

    public float GetRollCurrentDuration()
    {
        return _playerRollBehaviourSODatas.dictionary[_playerRollBehaviourSLData.level].duration;
    }

    public bool BuySuperJumpLevel(int coins)
	{
        if (IsLastSuperJumpLevel())
            return false;
        if (_coinDataManager.SpendCoins(coins))
		{
			_playerSuperJumpBehaviourSLData.level++;
			_playerBehaviourDataCombiner.SaveData(_playerSuperJumpBehaviourSLData);
            return true;
        }
		return false;
    }

    private bool IsLastSuperJumpLevel()
    {
        return _playerSuperJumpBehaviourSODatas.dictionary.Count == _playerBehaviourDataCombiner.playerSuperJumpBehaviourSLData.level + 1;
    }

    private bool IsLastRollJumpLevel()
    {
        return _playerRollBehaviourSODatas.dictionary.Count == _playerBehaviourDataCombiner.playerRollBehaviourSLData.level + 1;
    }

    public bool BuyRollLevel(int coins)
    {
        if (IsLastRollJumpLevel())
            return false;
        if (_coinDataManager.SpendCoins(coins))
        {
            _playerRollBehaviourSLData.level++;
			_playerBehaviourDataCombiner.SaveData(_playerRollBehaviourSLData);
            return true;
        }
        return false;
    }

    public UpgradeDataPackage GetUpgradeSuperJumpDataPackage()
    {
        UpgradeDataPackage upgradeDataPackage = new();
        upgradeDataPackage.currentLevel = _playerBehaviourDataCombiner.playerSuperJumpBehaviourSLData.level;
        upgradeDataPackage.currentDuration = _playerSuperJumpBehaviourSODatas.dictionary[_playerSuperJumpBehaviourSLData.level].duration;
        if (_playerSuperJumpBehaviourSODatas.dictionary.Count == upgradeDataPackage.currentLevel + 1)
        {
            upgradeDataPackage.isLastLevel = true;
            return upgradeDataPackage;
        }
        upgradeDataPackage.nextDuration = _playerSuperJumpBehaviourSODatas.dictionary[_playerSuperJumpBehaviourSLData.level + 1].duration;
        upgradeDataPackage.nextLevelCost = _playerSuperJumpBehaviourSODatas.dictionary[_playerSuperJumpBehaviourSLData.level + 1].cost;
        upgradeDataPackage.isLastLevel = false;
        return upgradeDataPackage;
    }

    public UpgradeDataPackage GetUpgradeRollDataPackage()
    {
        UpgradeDataPackage upgradeDataPackage = new();
        upgradeDataPackage.currentLevel = _playerBehaviourDataCombiner.playerRollBehaviourSLData.level;
        upgradeDataPackage.currentDuration = _playerRollBehaviourSODatas.dictionary[_playerRollBehaviourSLData.level].duration;
        if (_playerRollBehaviourSODatas.dictionary.Count == upgradeDataPackage.currentLevel + 1)
        {
            upgradeDataPackage.isLastLevel = true;
            return upgradeDataPackage;
        }
        upgradeDataPackage.nextDuration = _playerRollBehaviourSODatas.dictionary[_playerRollBehaviourSLData.level + 1].duration;
        upgradeDataPackage.nextLevelCost = _playerRollBehaviourSODatas.dictionary[_playerRollBehaviourSLData.level + 1].cost;
        upgradeDataPackage.isLastLevel = false;
        return upgradeDataPackage;
    }
}

public struct UpgradeDataPackage
{
    public int currentLevel;
    public float currentDuration;
    public float nextDuration;
    public int nextLevelCost;
    public bool isLastLevel;
}

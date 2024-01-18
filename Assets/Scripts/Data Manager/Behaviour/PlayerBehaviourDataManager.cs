using Zenject;
using static PlayerBehaviourDataManager;

public interface IPlayerBehaviourDataManager : IService
{
	public bool BuySuperJumpLevel(int coins);
	public bool BuyRollLevel(int coins);
    public UpgradeDataPackage GetUpgradeSuperJumpDataPackage();
}

public class PlayerBehaviourDataManager : IPlayerBehaviourDataManager
{
    private PlayerRollBehaviourSOData _playerRollBehaviourSOData;
    private PlayerRollBehaviourSLData _playerRollBehaviourSLData;
    private PlayerRollBehaviourSODatas _playerRollBehaviourSODatas;

    private PlayerSuperJumpBehaviourSOData _playerSuperJumpBehaviourSOData;
    private PlayerSuperJumpBehaviourSODatas _playerSuperJumpBehaviourSODatas;
    private PlayerSuperJumpBehaviourSLData _playerSuperJumpBehaviourSLData;

	private PlayerBehaviourDataCombiner _playerBehaviourDataCombiner;
	private ICoinDataManager _coinDataManager;
    private IPlayerBehaviourStorageData _playerBehaviourStorageData;

    [Inject]
	public void Constructor(PlayerBehaviourDataCombiner playerBehaviourDataCombiner,
							ICoinDataManager coinDataManager,
							IPlayerBehaviourStorageData playerBehaviourStorageData)
	{
		_coinDataManager = coinDataManager;
		_playerBehaviourDataCombiner = playerBehaviourDataCombiner;
		_playerBehaviourStorageData = playerBehaviourStorageData;
    }
	
	public void ActivateService()
	{
        _playerSuperJumpBehaviourSLData = _playerBehaviourDataCombiner.playerSuperJumpBehaviourSLData;
        _playerRollBehaviourSLData = _playerBehaviourDataCombiner.playerRollBehaviourSLData;
        _playerSuperJumpBehaviourSODatas = _playerBehaviourDataCombiner.GetPlayerSuperJumpBehaviourSODatas();
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

    public bool BuyRollLevel(int coins)
    {
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
}

public struct UpgradeDataPackage
{
    public int currentLevel;
    public float currentDuration;
    public float nextDuration;
    public int nextLevelCost;
    public bool isLastLevel;
}

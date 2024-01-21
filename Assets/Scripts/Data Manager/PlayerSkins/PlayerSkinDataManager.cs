using System.Collections.Generic;
using Zenject;

public interface IPlayerSkinDataManager
{
    public List<PlayerSkinData> GetPlayerSkinDatas();
}


public class PlayerSkinDataManager : IPlayerSkinDataManager
{
    private PlayerSkinDataCombiner _playerSkinDataCombiner;
    private List<PlayerSkinData> _playerSkinDatas;

    [Inject]
    public void Constructor(PlayerSkinDataCombiner playerSkinDataCombiner)
    {
        _playerSkinDataCombiner = playerSkinDataCombiner;
        _playerSkinDatas = _playerSkinDataCombiner.GetPlayerSkinDatas();
    }

    public List<PlayerSkinData> GetPlayerSkinDatas() => _playerSkinDatas;
}

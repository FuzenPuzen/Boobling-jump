using System;
using System.Collections.Generic;
using System.Linq;

public class SessionData : IPlayerRollBehaviourData, IPlayerSuperJumpBehaviourData, IPlayerCurrentBehaviourData
{
    public event Action RollBehaviourDataChanged;
    public event Action SuperJumpBehaviourDataChanged;

    private List<IPlayerBehaviourData> playerBehavioursDatas = new();

    public PlayerSuperJumpBehaviourSOData playerSuperJumpBehaviourSOData;

    public float GetRollBehaviourDuration()
    {
        return 0;
    }

    public PlayerSuperJumpBehaviourSOData GetSuperJumpBehaviourData()
    {
        return playerSuperJumpBehaviourSOData;
    }

    public void SetRollBehaviourDuration()
    {
    }

    public void SetSuperJumpBehaviourData(PlayerSuperJumpBehaviourSOData playerSuperJumpBehaviourSOData)
    {
        this.playerSuperJumpBehaviourSOData = playerSuperJumpBehaviourSOData;
    }

    public void FillData()
    {
        playerBehavioursDatas.Add(playerSuperJumpBehaviourSOData);
    }

    public void GetCurrentBehaviourData()
    {

    }

    public IPlayerBehaviourData GetPlayerCurrentBehaviourData<T>() where T : IPlayerBehaviourData
    {
        return playerBehavioursDatas.OfType<T>().FirstOrDefault();
    }

    public float GetDuration()
    {
        throw new NotImplementedException();
    }
}

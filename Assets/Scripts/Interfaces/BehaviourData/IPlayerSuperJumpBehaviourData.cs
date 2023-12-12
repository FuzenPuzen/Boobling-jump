using System;

public interface IPlayerSuperJumpBehaviourData
{
    public event Action SuperJumpBehaviourDataChanged;
    public void SetSuperJumpBehaviourData(PlayerSuperJumpBehaviourSOData playerSuperJumpBehaviourSOData);
    public PlayerSuperJumpBehaviourSOData GetSuperJumpBehaviourData();
}

using System;
public interface IPlayerRollBehaviourData : IPlayerBehaviourData
{
    public event Action RollBehaviourDataChanged;
    public void SetRollBehaviourDuration();
    public float GetRollBehaviourDuration();
}

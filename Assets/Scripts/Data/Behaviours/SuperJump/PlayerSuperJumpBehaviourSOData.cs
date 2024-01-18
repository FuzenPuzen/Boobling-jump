using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSuperJumpBehaviourSOData", menuName = "PlayerSuperJumpBehaviourSOData")]

public class PlayerSuperJumpBehaviourSOData : ScriptableObject, IPlayerBehaviourData
{
    public float duration;
    public int cost;
    public float GetDuration()
    {
        return duration;
    }
}

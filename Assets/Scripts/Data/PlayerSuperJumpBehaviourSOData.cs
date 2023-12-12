using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSuperJumpBehaviourSOData", menuName = "PlayerSuperJumpBehaviourSOData")]

public class PlayerSuperJumpBehaviourSOData : ScriptableObject, IPlayerBehaviourData
{
    public int level;
    public float duration;

    public float GetDuration()
    {
        return duration;
    }
}

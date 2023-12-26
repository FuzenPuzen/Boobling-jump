using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSuperJumpBehaviourSOData", menuName = "PlayerSuperJumpBehaviourSOData")]

public class PlayerSuperJumpBehaviourSOData : ScriptableObject, IPlayerBehaviourData
{
    public float duration;
    public float GetDuration()
    {
        return duration;
    }
}

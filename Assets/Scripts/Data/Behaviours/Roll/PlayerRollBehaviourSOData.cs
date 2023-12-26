using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRollBehaviourSOData", menuName = "PlayerRollBehaviourSOData")]
public class PlayerRollBehaviourSOData : ScriptableObject, IPlayerBehaviourData
{
    public float duration;

    public float GetDuration()
    {
        return duration;
    }
}

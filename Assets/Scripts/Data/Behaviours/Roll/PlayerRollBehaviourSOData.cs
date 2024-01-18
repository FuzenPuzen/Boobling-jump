using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRollBehaviourSOData", menuName = "PlayerRollBehaviourSOData")]
public class PlayerRollBehaviourSOData : ScriptableObject, IPlayerBehaviourData
{
    public float duration;
    public int cost;


    public float GetDuration()
    {
        return duration;
    }
}

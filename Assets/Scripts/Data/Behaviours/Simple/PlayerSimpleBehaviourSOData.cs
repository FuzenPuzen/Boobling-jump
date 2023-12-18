using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSimpleBehaviourSOData", menuName = "PlayerSimpleBehaviourSOData")]

public class PlayerSimpleBehaviourSOData : ScriptableObject, IPlayerBehaviourData
{
    public float duration;
    public float GetDuration()
    {
        return duration;
    }
}


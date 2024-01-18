using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStartBehaviourSOData", menuName = "PlayerStartBehaviourSOData")]
public class PlayerStartBehaviourSOData : ScriptableObject, IPlayerBehaviourData
{
    public float duration;
    public float cost;

    public float GetDuration()
    {
        return duration;
    }
}

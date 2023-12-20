using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStartBehaviourSODatas", menuName = "PlayerStartBehaviourSODatas")]
public class PlayerStartBehaviourSODatas : ScriptableObject
{
    [SerializedDictionary("Level", "StartLevel")]
    public SerializedDictionary<int, PlayerStartBehaviourSOData> dictionary;
}

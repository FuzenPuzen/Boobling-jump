using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSimpleBehaviourSODatas", menuName = "PlayerSimpleBehaviourSODatas")]
public class PlayerSimpleBehaviourSODatas : ScriptableObject
{
    [SerializedDictionary("Level", "SimpleJumpLevel")]
    public SerializedDictionary<int, PlayerSimpleBehaviourSOData> dictionary;
}

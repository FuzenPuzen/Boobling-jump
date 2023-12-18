using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "PlayerSuperJumpBehaviourSODatas", menuName = "PlayerSuperJumpBehaviourSODatas")]
public class PlayerSuperJumpBehaviourSODatas : ScriptableObject
{
    [SerializedDictionary("Level", "SuperJumpLevel")]
    public SerializedDictionary<int, PlayerSuperJumpBehaviourSOData> dictionary;
}

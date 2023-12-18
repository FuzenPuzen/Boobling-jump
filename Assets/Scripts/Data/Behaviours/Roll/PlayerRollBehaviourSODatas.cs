using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRollSODatas", menuName = "PlayerRollSODatas")]
public class PlayerRollBehaviourSODatas : ScriptableObject
{
    [SerializedDictionary("Level", "RollLevel")]
    public SerializedDictionary<int, PlayerRollBehaviourSOData> dictionary;
}

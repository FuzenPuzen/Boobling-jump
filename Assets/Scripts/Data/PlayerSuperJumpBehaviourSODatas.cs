using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "PlayerSuperJumpBehaviourSODatas", menuName = "PlayerSuperJumpBehaviourSODatas")]
public class PlayerSuperJumpBehaviourSODatas : ScriptableObject
{
    [SerializedDictionary("Id", "SuperJumpLevel")]
    public SerializedDictionary<int, PlayerSuperJumpBehaviourSOData> dictionary;

    [SerializeField]
    public int hrt { get; set; }
}

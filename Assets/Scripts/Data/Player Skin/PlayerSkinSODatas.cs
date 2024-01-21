using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkinSODatas", menuName = "PlayerSkinSODatas")]
public class PlayerSkinSODatas : ScriptableObject
{
    [SerializedDictionary("Id", "SkinPb")]
    public SerializedDictionary<int, PlayerSkinSOData> dictionary;
}

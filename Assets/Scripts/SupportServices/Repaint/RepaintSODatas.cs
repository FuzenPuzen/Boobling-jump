using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "RepaintDatas", menuName = "RepaintDatas")]
public class RepaintSODatas : ScriptableObject
{
    [SerializedDictionary("id", "RepaintData")]
    public SerializedDictionary<int, RepaintSOData> dictionary;

}


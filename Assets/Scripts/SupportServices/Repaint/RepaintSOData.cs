using UnityEngine;

[CreateAssetMenu(fileName = "RepaintData", menuName = "RepaintData")]
public class RepaintSOData : ScriptableObject
{
    public Color32 backgroundColor = new (255,255,255,255);
    public Color32 main = new(255, 255, 255, 255);
    public Color32 second = new(255, 255, 255, 255);
    public Color32 accent = new(255, 255, 255, 255);
}


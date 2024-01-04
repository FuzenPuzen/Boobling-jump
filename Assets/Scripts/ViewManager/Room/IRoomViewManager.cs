using UnityEditor.Experimental.GraphView;
using UnityEngine;

public interface IRoomViewManager
{
    public Transform GetCurrentScorePos();
    public Transform GetGiftScorePos();
    public Transform GetRecordScorePos();

    public Transform GetGiftBoxSpawnPos();
}
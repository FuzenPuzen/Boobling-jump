using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomView : MonoBehaviour
{
    [SerializeField] private Transform _currentScorePos;
    [SerializeField] private Transform _giftScorePos;
    [SerializeField] private Transform _recordScorePos;
    [SerializeField] private Transform _giftBoxSpawnPos;
    public Transform GetCurrentScorePos() => _currentScorePos;
    public Transform GetGiftScorePos() => _giftScorePos;
    public Transform GetRecordScorePos() => _recordScorePos;
    public Transform GetGiftBoxSpawnPos() => _giftBoxSpawnPos;
}

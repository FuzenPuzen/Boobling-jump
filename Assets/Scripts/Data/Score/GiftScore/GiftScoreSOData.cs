using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GiftScoreSOData", menuName = "GiftScoreSOData")]
public class GiftScoreSOData : ScriptableObject, IGiftScoreData
{
    public int GiftScoreSize;
}
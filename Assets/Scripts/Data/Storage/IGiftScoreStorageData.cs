using System.Collections;
using UnityEngine;

public interface IGiftScoreStorageData
{
    public IGiftScoreData GetGiftScoreData();
    public void SetGiftScoreData(IGiftScoreData giftScoreData);
}
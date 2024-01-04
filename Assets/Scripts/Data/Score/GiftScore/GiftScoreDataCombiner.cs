using Zenject;
using Unity.VisualScripting;
using UnityEngine;

public class GiftScoreDataCombiner
{
    private GiftScoreSOData _giftScoreSOData;
    private ISOStorageService _sOStorageService;
    private IGiftScoreStorageData _giftScoreStorageData;

    [Inject]
    public void Constructor(ISOStorageService sOStorageService, IGiftScoreStorageData giftScoreStorageData)
    {
        _sOStorageService = sOStorageService;
        _giftScoreStorageData = giftScoreStorageData;
        GetAndPullGiftScoreDataToStorage();
    }

    public void GetAndPullGiftScoreDataToStorage()
    {
        _giftScoreSOData = GetConvertedSO<GiftScoreSOData>();
        SetDataToStorage(_giftScoreSOData);
    }
    public T GetConvertedSO<T>()
    {
        return _sOStorageService.GetSOByType<T>().ConvertTo<T>();
    }

    public void SetDataToStorage(IGiftScoreData sOStorageService)
    {
        _giftScoreStorageData.SetGiftScoreData(sOStorageService);
    }

}
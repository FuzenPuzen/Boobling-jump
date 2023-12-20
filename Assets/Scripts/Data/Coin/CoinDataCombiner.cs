using UnityEngine;
using MLALib;
using Zenject;

public class CoinDataCombiner 
{
    ICoinsStoradeData _coinsStoradeData;
    private const string _coinsSLDataKey = "coinsSLDataKey";
    CoinsSLData _coinsSLData;

    [Inject]
    public void Constructor(ICoinsStoradeData coinsStoradeData)
    {
        _coinsStoradeData = coinsStoradeData;
        _coinsStoradeData.CoinDataChanged += SaveCoinData;
    }

    private void GetAndPullSimpleDataToStorage()
    {
        _coinsSLData = new();
        _coinsSLData = SaveLoader.LoadData<CoinsSLData>(_coinsSLData, _coinsSLDataKey);
        _coinsStoradeData.SetCoinsSLData(_coinsSLData);
    }

    private void SaveCoinData(ICoinData coinData)
    {
        SaveLoader.SaveItem<CoinsSLData>((CoinsSLData)coinData, _coinsSLDataKey);
    }
}

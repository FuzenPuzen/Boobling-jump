using System;
using System.Diagnostics;
using UnityEngine;
using Zenject;

public class CoinDataManager : ICoinDataManager
{
    private ICoinsStorageData _coinsStoradeData;
    private CoinsSLData _coinsSLData;
    public event Action<int> coinsChanged;

    [Inject]
    public void constructor(ICoinsStorageData coinsStoradeData)
    {
        _coinsStoradeData = coinsStoradeData;
        _coinsSLData = (CoinsSLData)_coinsStoradeData.GetCoinsSLData();
    }

    public void CoinsChanged()
    {
        coinsChanged?.Invoke(_coinsSLData.coins);
        _coinsStoradeData.SetCoinsSLData(_coinsSLData);
    }

    public void AddCoins(int coins)
    {
        _coinsSLData.coins += coins;
        CoinsChanged();
    }

    public int GetCoins() => _coinsSLData.coins;

    public bool SpendCoins(int coins)
    {
        MonoBehaviour.print(_coinsSLData.coins);
        if (_coinsSLData.coins >= coins)
        {
            _coinsSLData.coins -= coins;
            CoinsChanged();
            MonoBehaviour.print(_coinsSLData.coins);
            return true; 
        }
        return false;
    }

    public void CollectCoins(int coins = 1)
    {
        _coinsSLData.coins += coins;
        CoinsChanged();
    }
}

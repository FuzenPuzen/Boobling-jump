using System;
using System.Diagnostics;
using UnityEngine;
using Zenject;

public interface ICoinDataManager
{
    public event Action<int> coinsChanged;
    public int GetCoins();
    public bool SpendCoins(int coins);
    public void AddCoins(int coins);

    public void CollectCoins(int coins = 1);
    public int GetSesionCollectedCoins();

}

public class CoinDataManager : ICoinDataManager
{
    private ICoinsStorageData _coinsStoradeData;
    private CoinsSLData _coinsSLData;
    public event Action<int> coinsChanged;
    private int _sessionCollectedCoins;
    private AlertPanelViewService _alertPanelViewService;

    [Inject]
    public void constructor(ICoinsStorageData coinsStoradeData,
                            AlertPanelViewService alertPanelViewService)
    {
        _alertPanelViewService = alertPanelViewService;
        _sessionCollectedCoins = 0;
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
        if (_coinsSLData.coins >= coins)
        {
            _coinsSLData.coins -= coins;
            CoinsChanged();
            return true; 
        }
        _alertPanelViewService.ShowView();
        return false;
    }

    public void CollectCoins(int coins = 1)
    {
        _sessionCollectedCoins += coins;
        _coinsSLData.coins += coins;
        CoinsChanged();
    }

    public int GetSesionCollectedCoins() => _sessionCollectedCoins;
}

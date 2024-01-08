using System.Collections.Generic;
using Zenject;

public class CoinPoolViewManager : IService
{
    private List<CoinView> Coins = new();
    private CoinView _coinView;
    private IViewFabric _fabric;
    private int CoinsInPool = 20; //перенести в coinData

    [Inject]
    public void Constructor(IViewFabric fabric)
    {
        _fabric = fabric;
    }

    public void ActivateService()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < CoinsInPool; i++)
        {
            _coinView = _fabric.SpawnObject<CoinView>();
            Coins.Add(_coinView);
            _coinView.gameObject.SetActive(false);
        }
    }

    public CoinView GetCoinFromPool()
    {
        for (int i = 0; i < Coins.Count; i++)
        {
            if (!Coins[i].gameObject.activeSelf)
            {
                Coins[i].gameObject.SetActive(true);
                return Coins[i];
            }
        }
        return null;
    }

    public void ReturnCoinToPool(CoinView coinView)
    {
        coinView.gameObject.SetActive(false);
    }
}

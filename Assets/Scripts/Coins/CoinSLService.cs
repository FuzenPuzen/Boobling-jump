using Zenject;

public class CoinSLService : Iservice
{
    private CoinData _coinData = new();
    private CoinView _coinView;

    [Inject]
    public void Constructor(IFabric fabric)
    {
        _coinView = fabric.SpawnObjectAndGetType<CoinView>();
    }

    public void ActivateService()
    {
        _coinView.SetCoinData(_coinData);
    }

    public void AddCoin(int? coins = 1)
    {
        
    }

    public void SpendCoin(int coins)
    {
        
    }

   
}

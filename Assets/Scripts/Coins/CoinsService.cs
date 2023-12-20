using Zenject;

public class CoinsService : Iservice
{
    private CoinView _coinView;
    private ICoinManager _coinManager;

    [Inject]
    public void Constructor(IFabric fabric, ICoinsStoradeData coinsStoradeData, ICoinManager coinManager)
    {
        _coinManager = coinManager;
        _coinView = fabric.SpawnObjectAndGetType<CoinView>();
        _coinManager.coinsChanged += UpdateView;
    }

    public void ActivateService()
    {
        UpdateView(_coinManager.GetCoins());
    }

    private void UpdateView(int coins)
    {
        _coinView.UpdateView(coins);
    }
  
}

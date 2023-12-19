using Zenject;

public class CoinsService : Iservice
{

    private CoinView _coinView;
    private ICoinsStoradeData _coinsStoradeData;

    [Inject]
    public void Constructor(IFabric fabric, ICoinsStoradeData coinsStoradeData)
    {
        _coinView = fabric.SpawnObjectAndGetType<CoinView>();
        _coinsStoradeData = coinsStoradeData;
    }

    public void ActivateService()
    {
        _coinView.SetCoinData(_coinsStoradeData.GetCoinsSLData());
    }

    private void UpdateView()
    {
        _coinView.UpdateView();
    }
  
}

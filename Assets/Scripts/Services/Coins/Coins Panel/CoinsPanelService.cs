using Zenject;

public class CoinsPanelService : Iservice
{
    private CoinsPanelView _coinView;
    private ICoinDataManager _coinManager;
    private IFabric _fabric;

    [Inject]
    public void Constructor(IFabric fabric, ICoinDataManager coinManager)
    {
        _fabric = fabric;
        _coinManager = coinManager;       
    }

    public void ActivateService()
    {
        _coinView = _fabric.SpawnObjectAndGetType<CoinsPanelView>();
        _coinManager.coinsChanged += UpdateView;
        UpdateView(_coinManager.GetCoins());
    }

    private void UpdateView(int coins)
    {
        _coinView.UpdateView(coins);
    }
  
}

using Zenject;

public class CoinsPanelService : IService
{
    private CoinsPanelView _coinView;
    private ICoinDataManager _coinManager;
    private IViewFabric _fabric;

    [Inject]
    public void Constructor(IViewFabric fabric, ICoinDataManager coinManager)
    {
        _fabric = fabric;
        _coinManager = coinManager;       
    }

    public void ActivateService()
    {
        _coinView = _fabric.Init<CoinsPanelView>();
        _coinManager.coinsChanged += UpdateView;
        UpdateView(_coinManager.GetCoins());
    }

    private void UpdateView(int coins)
    {
        _coinView.UpdateView(coins);
    }
  
}

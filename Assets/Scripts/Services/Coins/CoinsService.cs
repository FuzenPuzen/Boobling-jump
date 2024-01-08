using Zenject;

public class CoinsService : IService
{
    private ICoinDataManager _coinManager;
    private ITimerService _timerService;
    private CoinPoolViewManager _coinsPoolService;

    private CoinView _coinView;
    private int _lineCoins;
    private int _lineCoinsCount = 5;

    [Inject]
    public void Constructor(ICoinDataManager coinManager, CoinPoolViewManager coinsPoolService, ITimerService timerService)
    {
        _timerService = timerService;
        _coinsPoolService = coinsPoolService;
        _coinManager = coinManager;
    }

    public void ActivateService()
    {
        LinePlacement();
    }

    private void CoinCollectAction(CoinView coinView)
    {
        _coinManager.CollectCoins();
        _coinsPoolService.ReturnCoinToPool(coinView);
    }

    public void LinePlacement()
    {
        if (_lineCoins < _lineCoinsCount)
        {
            _coinView = _coinsPoolService.GetCoinFromPool();
            _coinView.transform.position = new(4.8f, 2, 0);
            _coinView.SetCollectAction(CoinCollectAction);
            _lineCoins++;
            _timerService.SetActionOnTimerComplete(1, LinePlacement);
        }
        else
        {
            _lineCoins = 0;
        }
    }
}

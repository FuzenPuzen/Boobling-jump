using Zenject;

public class CoinsService : Iservice
{
    private ICoinManager _coinManager;
    private ITimerService _timerService;
    private CoinsPoolService _coinsPoolService;

    private CoinView _coinView;
    private int _lineCoins;
    private int _lineCoinsCount = 5;

    [Inject]
    public void Constructor(ICoinManager coinManager, CoinsPoolService coinsPoolService, ITimerService timerService)
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
            _coinView.transform.position = new(-9, 2, 0);
            _coinView.SetCollectAction(CoinCollectAction);
            _coinView.StartMove();
            _lineCoins++;
            _timerService.SetActionOnTimerComplete(1, LinePlacement);
        }
        else
        {
            _lineCoins = 0;
        }
    }
}

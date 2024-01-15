using Zenject;

public interface IPoolsViewService : IService
{
	public IPoolViewService GetCoinPoolViewService();
	public IPoolViewService GetBonusPoolViewService();
}

public class PoolsViewService : IPoolsViewService
{
	private IServiceFabric _serviceFabric;
	private IPoolViewService _coinPoolViewService;
	private IPoolViewService _bonusPoolViewService;

    [Inject]
	public void Constructor(IServiceFabric serviceFabric)
	{
        _serviceFabric = serviceFabric;
    }
	
	public void ActivateService()
	{
		InitPools();
    }

	private void InitPools()
	{
		_coinPoolViewService = _serviceFabric.Create<PoolViewService>();
		_coinPoolViewService.SpawPool<DropedCoinViewService>(200);

        _bonusPoolViewService = _serviceFabric.Create<PoolViewService>();
    }

	// Вынести создание пула в 1 метод

	public IPoolViewService GetCoinPoolViewService()
	{
		return _coinPoolViewService;
	}

    public IPoolViewService GetBonusPoolViewService()
    {
        return _bonusPoolViewService;
    }

}

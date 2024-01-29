using Zenject;
using EventBus;

public interface ISuperJumpWavesService : IService
{

}

public class SuperJumpWavesService : ISuperJumpWavesService
{
    private IMarkerService _markerService;
    private IPoolsViewService _poolsViewService;
    private IPoolViewService _WavePoolViewService;
    private EventBinding<OnSupperJumpFall> _onSupperJumpFall;

    [Inject]
	public void Constructor(IMarkerService markerService,
                             IPoolsViewService poolsViewService)
	{
        _poolsViewService = poolsViewService;
        _markerService = markerService;
    }
	
	public void ActivateService()
	{
        _WavePoolViewService = _poolsViewService.GetPool<SuperJumpWaveViewService>();
        _onSupperJumpFall = new(ActivateWave);
    }

    private void ActivateWave()
    {
        _WavePoolViewService.GetItem().ActivateService();
    }
    public void DeactivateService()
    {
        _onSupperJumpFall.Remove(ActivateWave);
    }
}

using Zenject;
using EventBus;

public interface IAnimationService : IService
{
	public void CameraShake();
	public void DeactivateService();
}

public class AnimationService : IAnimationService
{
	private MainCameraViewService _mainCameraViewService;
	private EventBinding<OnPlayer—rashed> _onPlayer—rashed;
	private EventBinding<OnSupperJumpFall> _onSupperJumpFall;

    [Inject]
	public void Constructor(MainCameraViewService mainCameraViewService)
	{
        _mainCameraViewService = mainCameraViewService;
    }
	
	public void ActivateService()
	{
		_onPlayer—rashed = new(CameraShake);
        _onSupperJumpFall = new(CameraShake);
    }

	public void DeactivateService()
	{
		_onPlayer—rashed.Remove(CameraShake);
        _onSupperJumpFall.Remove(CameraShake);
    }

	public void CameraShake()
	{
        _mainCameraViewService.CameraShake();
    }

}

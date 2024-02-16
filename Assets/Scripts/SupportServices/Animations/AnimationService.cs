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
	private EventBinding<OnPlayergriped> _onPlayergriped;

    [Inject]
	public void Constructor(MainCameraViewService mainCameraViewService)
	{
        _mainCameraViewService = mainCameraViewService;
    }
	
	public void ActivateService()
	{
		_onPlayergriped = new(CameraShake);
    }

	public void DeactivateService()
	{
		_onPlayergriped.Remove(CameraShake);
    }

	public void CameraShake()
	{
        _mainCameraViewService.CameraShake();
    }

}

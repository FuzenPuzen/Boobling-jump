using Zenject;
using EventBus;

public interface ITutorialEndService : IService
{

}

public class TutorialEndService : ITutorialEndService
{
	[Inject]
	public void Constructor()
	{
	
	}
	
	public void ActivateService()
	{       
        
	}
}

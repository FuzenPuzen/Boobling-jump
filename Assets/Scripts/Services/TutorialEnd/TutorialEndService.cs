using Zenject;
using EventBus;

public interface ITutorialEndService : IService
{

}

public class TutorialEndService : ITutorialEndService
{
    private TutorialEndPageViewService _tutorialEndPageViewService;
    private ISessionTypeDataManager _sessionTypeDataManager;
    private IScoreDataManager _scoreDataManager;
    private ICoinDataManager _coinDataManager;

    [Inject]
	public void Constructor(ISessionTypeDataManager sessionTypeDataManager, 
                            TutorialEndPageViewService tutorialEndPageViewService,
                            IScoreDataManager scoreDataManager,
                            ICoinDataManager coinDataManager)
	{
        _scoreDataManager = scoreDataManager;
        _coinDataManager = coinDataManager;
        _sessionTypeDataManager = sessionTypeDataManager;
        _tutorialEndPageViewService = tutorialEndPageViewService;
    }
	
	public void ActivateService()
	{
        int rewardCount = _scoreDataManager.GetScoreRewardDataPackage().RewardCount;
        _coinDataManager.AddCoins(rewardCount);
        _sessionTypeDataManager.SaveGameType(false);
        _tutorialEndPageViewService.ActivateService();
    }
}

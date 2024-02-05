using Zenject;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using EventBus;

public class TutorialEndPanelView : MonoBehaviour
{
	[SerializeField] private TMP_Text _collectedCoins;
	[SerializeField] private TMP_Text _receivedCoins;
	[SerializeField] private Button _menuButton;
	[SerializeField] private Button _newChapterButton;

    public void Start()
    {
		_menuButton.onClick.AddListener(ToMenu);
		_newChapterButton.onClick.AddListener(ToNewChapter);
    }

	public void ToMenu()
	{
        EventBus<OnOpenMenu>.Raise();
    }

	public void ToNewChapter()
	{
        EventBus<OnRestart>.Raise();
    }

	public void SetData(int collectedCoins, int receivedCoins)
	{
        _collectedCoins.text = collectedCoins.ToString();
		_receivedCoins.text = receivedCoins.ToString();
    }

}

public class TutorialEndPanelViewService : IService
{
	private IViewFabric _fabric;
	private TutorialEndPanelView _TutorialEndPanelView;
    private IMarkerService _markerService;

    private IScoreDataManager _scoreDataManager;
    private ICoinDataManager _coinDataManager;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService,
							IScoreDataManager scoreDataManager,
							ICoinDataManager coinDataManager)
	{
        _scoreDataManager = scoreDataManager;
		_coinDataManager = coinDataManager;
        _markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _TutorialEndPanelView = _fabric.Init<TutorialEndPanelView>();
		_TutorialEndPanelView.SetData(_coinDataManager.GetSesionCollectedCoins(), _scoreDataManager.GetScoreRewardDataPackage().RewardCount);

    }
}

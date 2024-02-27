using Zenject;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using EventBus;
using DG.Tweening;

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
        DOTween.To(() => 0, x => _collectedCoins.text = x.ToString(), collectedCoins, 1f);
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
		Transform parent = _markerService.GetTransformMarker<TutorialPageMarker>().transform;
        _TutorialEndPanelView = _fabric.Init<TutorialEndPanelView>(parent);
		_TutorialEndPanelView.SetData(_scoreDataManager.GetScoreRewardDataPackage().RewardCount, _coinDataManager.GetSesionCollectedCoins());
    }
}

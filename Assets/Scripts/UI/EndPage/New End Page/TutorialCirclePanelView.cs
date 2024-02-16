using Zenject;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TutorialCirclePanelView : MonoBehaviour
{
    [SerializeField] private GameObject _remaindPanel;
    private Sequence _fillSequence;
    private float _fillTime = 1f;
    [SerializeField] private Image _filledImage;
    private float _currentFill;

    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private TMP_Text _missingScoreText;
    private int _recordScore;
    private int _rewardScore;
    private ScoreRewardDataPackage _rewardDataPackage;

    public void SetData(ScoreRewardDataPackage rewardDataPackage, int record)
    {
        _rewardDataPackage = rewardDataPackage;
        _rewardScore = _rewardDataPackage.RewardScore;
        _recordScore = record;
        FillingCircle();
    }

    public void FillingCircle()
    {
        _currentScoreText.text = _recordScore.ToString();
        _fillSequence = DOTween.Sequence();
        _currentFill = (float)_recordScore / (float)_rewardDataPackage.RewardScore;
        _fillSequence.Append(_filledImage.DOFillAmount(_currentFill, _fillTime)).OnComplete(fillRemaindPanel);
    }

    public void fillRemaindPanel()
    {
        _remaindPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        _missingScoreText.text = (_rewardScore - _recordScore).ToString();
    }
}

public class TutorialCirclePanelViewService : IService
{
	private IViewFabric _fabric;
	private TutorialCirclePanelView _TutorialCirclePanelView;
    private IMarkerService _markerService;
    private IScoreDataManager _scoreDataManager;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService, IScoreDataManager scoreDataManager)
	{
        _scoreDataManager = scoreDataManager;
        _markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
        Transform parent = _markerService.GetTransformMarker<EndPageMarker>().transform;
        _TutorialCirclePanelView = _fabric.Init<TutorialCirclePanelView>(parent);
        _TutorialCirclePanelView.SetData(_scoreDataManager.GetScoreRewardDataPackage(), _scoreDataManager.GetRecordScore());

    }
}

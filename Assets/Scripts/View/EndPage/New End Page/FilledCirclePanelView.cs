using Zenject;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class FilledCirclePanelView : MonoBehaviour
{
    [SerializeField] private GameObject _bonusPanel;
    [SerializeField] private GameObject _remaindPanel;
    private Sequence _fillSequence;
    private float _fillTime = 1f;
    private float _fillLoopTime;

    [SerializeField] private Image _filledImage;
    private float _currentFill;

    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private TMP_Text _missingScoreText;
    private int _currentScore;
    private int _totalScore;
    private int _remaidScore;
    private int _rewardScore;
    private int _rewardMultiplayer;
    private int _currentMultiplayer;
    [SerializeField] private TMP_Text _rewardMultiplayerText;

    [SerializeField] private TMP_Text _rewardCoinsText;


    private ScoreRewardDataPackage _rewardDataPackage;

    public void SetData(ScoreRewardDataPackage scoreRewardDataPackage)
    {
        _rewardDataPackage = scoreRewardDataPackage;
        FilledCirclePanel();
    }

    public void FilledCirclePanel()
    {
        _currentMultiplayer = 0;
        _rewardScore = _rewardDataPackage.RewardScore;
        _remaidScore = _rewardDataPackage.RemaindScore;
        _currentScore = _rewardDataPackage.CurrentScore;

        _currentFill = (float)_remaidScore / (float)_rewardScore;
        _filledImage.fillAmount = _currentFill;

        _totalScore = _remaidScore + _currentScore;
        _rewardMultiplayer = _totalScore / _rewardScore;

        _fillLoopTime = _fillTime / (_rewardMultiplayer + 1);
        FillScoreText();
        FillCircle(_rewardMultiplayer);
    }

    public void FillScoreText()
    {
        _currentScoreText.text = _totalScore.ToString();
        float scoreReduceTime = _fillTime * 2 - _fillLoopTime;
        DOTween.To(() => _totalScore, x => _currentScoreText.text = x.ToString(), 0, scoreReduceTime);
    }

    public void FillCircle(int multiplier)
    {
        if (multiplier <= 0)
            FillingCircle();
        else
            LoopedFill(multiplier);
    }

    public void FillingCircle()
    {
        _fillSequence = DOTween.Sequence();
        _currentFill = (float)_totalScore / (float)_rewardScore - _rewardMultiplayer;
        _fillSequence.Append(_filledImage.DOFillAmount(_currentFill, _fillTime)).OnComplete(fillRemaindPanel);
    }

    public void LoopedFill(int loops)
    {
        _fillSequence = DOTween.Sequence();
        if (loops == 0)
        {
            _currentFill = (float)_totalScore / (float)_rewardScore - _rewardMultiplayer;
            _fillSequence.Append(_filledImage.DOFillAmount(_currentFill, _fillTime)).OnComplete(FillBonusPanel);
            _rewardMultiplayerText.transform.localScale = Vector3.one * 2.0f;
            _rewardMultiplayerText.transform.DOPunchScale(Vector3.one * 2.1f, _fillLoopTime, 4, 0.5f);
            return;
        }
        _fillSequence.Append(_filledImage.DOFillAmount(1, _fillLoopTime)).OnComplete(() => CircleFulledFill(loops - 1));
    }

    public void CircleFulledFill(int loops)
    {
        _rewardMultiplayerText.gameObject.SetActive(true);
        _currentMultiplayer++;
        if (_currentMultiplayer > 3) _rewardMultiplayerText.color = Color.green;
        if (_currentMultiplayer > 5) _rewardMultiplayerText.color = Color.blue;
        if (_currentMultiplayer > 10) _rewardMultiplayerText.color = new Color32(117, 114, 209, 255);
        if (_currentMultiplayer > 15) _rewardMultiplayerText.color = Color.red;
        _rewardMultiplayerText.text = "X" + _currentMultiplayer;
        _rewardMultiplayerText.transform.DOPunchScale(Vector3.one * 1.2f, _fillLoopTime, 6, 0.5f);
        _fillSequence.Kill();
        _filledImage.fillAmount = 0;
        LoopedFill(loops);
    }

    private void FillBonusPanel()
    {
        _bonusPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        _rewardCoinsText.text = (_rewardDataPackage.RewardCount * _rewardMultiplayer).ToString();
    }

    private void fillRemaindPanel()
    {
        _remaindPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        _missingScoreText.text = (_rewardScore - _totalScore).ToString();
    }
}

public class FilledCirclePanelViewService : IService
{
	private IViewFabric _fabric;
    private IServiceFabric _serviceFabric;
    private FilledCirclePanelView _FilledCirclePanel;
    private IMarkerService _markerService;

    private IScoreDataManager _scoreDataManager;


    [Inject]
    public void Constructor(IViewFabric viewFabric, IMarkerService markerService,
                            IServiceFabric serviceFabric,
                            IScoreDataManager scoreDataManager)
    {
        _markerService = markerService;
        _fabric = viewFabric;
        _serviceFabric = serviceFabric;
        _scoreDataManager = scoreDataManager;
    }

    public void ActivateService()
	{
        Transform parent = _markerService.GetTransformMarker<EndPageMarker>().transform;
        _FilledCirclePanel = _fabric.Init<FilledCirclePanelView>(parent);
        _FilledCirclePanel.SetData(_scoreDataManager.GetScoreRewardDataPackage());
    }
}

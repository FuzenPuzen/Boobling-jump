using Zenject;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GetBonusPanelView : MonoBehaviour
{
    [SerializeField] private Button _selectBonusTypeButton;
    [SerializeField] private Button _getDefaultRewardButton;
    [SerializeField] private Button _getBonusRewardButton;
    public Action onSelectBonusTypeAction;
    public Action<RewardType> onGetRewardAction;
    public void Awake()
    {
        gameObject.SetActive(false);
        _selectBonusTypeButton.onClick.AddListener(OnSelectBonusType);
        _getDefaultRewardButton.onClick.AddListener(GetDeafaultReward);
        _getBonusRewardButton.onClick.AddListener(GetBonusReward);
    }
    public void ActivateView()
    {
        
    }

    public void ShowView()
    {
        gameObject.SetActive(true);
    }

    public void HideView()
    {
        gameObject.SetActive(false);
    }

    private void OnSelectBonusType()
    {
        _selectBonusTypeButton.gameObject.SetActive(false);
        onSelectBonusTypeAction?.Invoke();
    }

    private void GetDeafaultReward()
    {
        onGetRewardAction?.Invoke(RewardType.Default);
    }

    private void GetBonusReward()
    {
        onGetRewardAction?.Invoke(RewardType.Bonus);
    }
}

public class GetBonusPanelViewService : IService
{
	private IViewFabric _viewFabric;
    private IServiceFabric _serviceFabric;
    private GetBonusPanelView _getBonusPanelView;
    private RewardBonusTypeViewService _rewardBonusTypeViewService;
    private IMarkerService _markerService;
    private Action _getBonusCompleteAction;
    private RewardBonusType _rewardBonusType;

    [Inject]
	public void Constructor(
        IViewFabric viewFabric,
        IMarkerService markerService,
        IServiceFabric serviceFabric)
	{
		_markerService = markerService;
		_viewFabric = viewFabric;
        _serviceFabric = serviceFabric;
	}

	public void ActivateService()
	{
		Transform parent = _markerService.GetTransformMarker<EndPageMarker>().transform;
        _getBonusPanelView = _viewFabric.Init<GetBonusPanelView>(parent);
		_getBonusPanelView.ActivateView();
        _getBonusPanelView.onSelectBonusTypeAction = OnSelectBonusType;
        _getBonusPanelView.onGetRewardAction = OnGetReward;
        _rewardBonusTypeViewService = _serviceFabric.InitSingle<RewardBonusTypeViewService>();
        HideView();

    }

	public void ShowView()
	{
		_getBonusPanelView.ShowView();
        _rewardBonusTypeViewService.ActivateService();

    }
    public void HideView()
    {
        _getBonusPanelView.HideView();

    }
    private void OnSelectBonusType()
    {
        _rewardBonusType = _rewardBonusTypeViewService.StopChangeType();
    }

    private void OnGetReward(RewardType type)
    {
        _getBonusCompleteAction?.Invoke();
    }

    public void SetOnGetBonusCompleteAction(Action action)
    {
        _getBonusCompleteAction = action;
    }
}

public enum RewardType
{
    Default,
    Bonus
}



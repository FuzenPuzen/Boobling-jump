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
	private IViewFabric _fabric;
	private GetBonusPanelView _getBonusPanelView;
    private IMarkerService _markerService;
    private Action _getBonusCompleteAction;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
		Transform parent = _markerService.GetTransformMarker<EndPageMarker>().transform;
        _getBonusPanelView = _fabric.Init<GetBonusPanelView>(parent);
		_getBonusPanelView.ActivateView();
        _getBonusPanelView.onSelectBonusTypeAction = OnSelectBonusType;
        _getBonusPanelView.onGetRewardAction = OnGetReward;
    }

	public void ShowView()
	{
		_getBonusPanelView.ShowView();

    }
    public void HideView()
    {
        _getBonusPanelView.HideView();

    }
    private void OnSelectBonusType()
    {
        //selectbonustype button hide in view
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

public enum RewardBonusType
{
    X2 = 2,
    X3 = 3,
    X5 = 5
}

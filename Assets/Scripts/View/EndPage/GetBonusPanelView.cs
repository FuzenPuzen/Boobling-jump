using Zenject;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GetBonusPanelView : MonoBehaviour
{
    [SerializeField] private Button _splashStopButton;
    [SerializeField] private Button _getDefaultRewardButton;
    [SerializeField] private Button _getBonusRewardButton;
    public Action onSelectBonusTypeAction;
    public void Awake()
    {
        gameObject.SetActive(false);
        _splashStopButton.onClick.AddListener(OnSelectBonusType);
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
        _splashStopButton.gameObject.SetActive(false);
        onSelectBonusTypeAction?.Invoke();
    }

    private void GetDeafaultReward()
    {

    }

    private void GetBonusReward()
    {

    }
}

public class GetBonusPanelViewService : IService
{
	private IViewFabric _fabric;
	private GetBonusPanelView _getBonusPanelView;
    private IMarkerService _markerService;
	
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
        
    }

    private void GetReward()
    {

    }
}

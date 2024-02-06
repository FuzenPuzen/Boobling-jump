using Zenject;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AlertPanelView : MonoBehaviour
{
	[SerializeField] private Button _submitButton;
	public Action SubmitAction;

    public void Start()
    {
		_submitButton.onClick.AddListener(() => SubmitAction?.Invoke());
    }

    internal void HideView()
    {
        gameObject.SetActive(false);
    }

    internal void ShowView()
    {
        gameObject.SetActive(true);
    }
}

public class AlertPanelViewService : IService
{
	private IViewFabric _fabric;
	private AlertPanelView _alertPanelView;
    private IMarkerService _markerService;
    private ICoinDataManager _coinDataManager;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService,
                            ICoinDataManager coinDataManager)
	{
        _coinDataManager = coinDataManager;
        _markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
        _alertPanelView = _fabric.Init<AlertPanelView>();
		_alertPanelView.SubmitAction = SubmitAction;
        HideView();
    }

	public void SubmitAction()
	{
        _coinDataManager.AddCoins(1);
        HideView();
    }

    public void HideView()
    {
        _alertPanelView.HideView();
    }

    public void ShowView()
    {
        _alertPanelView.ShowView();
    }
}

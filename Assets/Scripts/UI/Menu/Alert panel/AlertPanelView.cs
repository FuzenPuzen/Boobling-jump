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
	private AlertPanelView _AlertPanelView;
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
        _AlertPanelView = _fabric.Init<AlertPanelView>();
		_AlertPanelView.SubmitAction = SubmitAction;
    }

	public void SubmitAction()
	{
        _coinDataManager.AddCoins(1);
        HideView();
    }

    public void HideView()
    {
        _AlertPanelView.HideView();
    }

    public void ShowView()
    {
        _AlertPanelView.ShowView();
    }
}

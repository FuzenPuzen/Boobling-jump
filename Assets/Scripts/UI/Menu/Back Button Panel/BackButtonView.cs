using Zenject;
using UnityEngine;
using UnityEngine.UI;
using System;
using EventBus;

public class BackButtonView : MonoBehaviour
{

	[SerializeField] private Button _menuButton;

    private void Start()
    {
		_menuButton.onClick.AddListener(GetBack);
    }

    private void GetBack()
	{
        EventBus<OnClickMenu>.Raise();
    }
}

public class BackButtonViewService : IService
{
	private IViewFabric _fabric;
	private BackButtonView _BackButtonView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
		Transform parent = _markerService.GetTransformMarker<BackButtonPanelMarker>().transform;
        _BackButtonView = _fabric.Init<BackButtonView>(parent);
	}
}

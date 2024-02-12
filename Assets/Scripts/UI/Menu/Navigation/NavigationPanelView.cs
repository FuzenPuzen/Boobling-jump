using Zenject;
using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;

public class NavigationPanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _panelName;
    internal void SetPanelName(string name)
    {
        _panelName.text = name;
    }
}

public class NavigationPanelViewService : IService
{
	
	private IViewFabric _fabric;
	private NavigationPanelView _NavigationPanelView;
	private IMarkerService _markerService;
	private Dictionary<PanelName, string> _panelNames = new()
	{ 
		[PanelName.Menu] = "Меню",
		[PanelName.Upgrage] = "Прокачка",
		[PanelName.Skin] = "Скинчики"
	};
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
		Transform parent = _markerService.GetTransformMarker<NavigationCanvasMarker>().transform;
        _NavigationPanelView = _fabric.Init<NavigationPanelView>(parent);
	}

    internal void SetPanelName(PanelName panelName)
    {
        _NavigationPanelView.SetPanelName(_panelNames[panelName]);
    }
}

public enum PanelName
{ 
	Menu,
	Upgrage,
	Skin
}

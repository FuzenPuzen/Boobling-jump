using Zenject;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class EndChoosePanelView : MonoBehaviour
{
    [SerializeField] private Button _menuOpenButton;
    [SerializeField] private Button _restartButton;
    public Action OnMenuOpen;
    public Action OnRestart;
    public void Awake()
    {
        gameObject.SetActive(false);
    }
    public void ActivateView()
    {
        _menuOpenButton.onClick.AddListener(() => OnMenuOpen?.Invoke());
        _restartButton.onClick.AddListener(() => OnRestart?.Invoke());
    }

    public void ShowView()
    {
        gameObject.SetActive(true);
    }

    public void HideView()
    {
        gameObject.SetActive(false);
    }
}

public class EndChoosePanelViewService : IService
{
	private IViewFabric _fabric;
	private EndChoosePanelView _endChoosePanelView;
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
        _endChoosePanelView = _fabric.Init<EndChoosePanelView>(parent);
        _endChoosePanelView.ActivateView();
        _endChoosePanelView.OnMenuOpen = OnMenuOpen;
        _endChoosePanelView.OnRestart = OnRestart;
        HideView();

    }
    public void ShowView()
    {
        _endChoosePanelView.ShowView();

    }
    public void HideView()
    {
        _endChoosePanelView.HideView();

    }

    private void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void OnMenuOpen()
    {
        SceneManager.LoadScene(0);
    }


}

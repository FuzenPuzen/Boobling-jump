using Zenject;
using UnityEngine;
using TMPro;

public class MenuCoinPanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;

    public void UpdateView(int coins)
    {
        _coinsText.text = coins.ToString();
    }
}

public class MenuCoinPanelViewService : IService
{
	private IViewFabric _fabric;
	private MenuCoinPanelView _MenuCoinPanelView;
    private ICoinDataManager _coinDataManager;
    private IMarkerService _markerService;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService, ICoinDataManager coinDataManager)
	{
        _coinDataManager = coinDataManager;
        _markerService = markerService;
        _fabric = fabric;
        _coinDataManager.coinsChanged += UpdateView;
    }

	public void ActivateService()
	{
        _MenuCoinPanelView = _fabric.Init<MenuCoinPanelView>(_markerService.GetTransformMarker<NavigationPanelMarker>().transform);
        UpdateView(_coinDataManager.GetCoins());
    }

    public void UpdateView(int coins)
    {
        _MenuCoinPanelView.UpdateView(coins);
    }
}

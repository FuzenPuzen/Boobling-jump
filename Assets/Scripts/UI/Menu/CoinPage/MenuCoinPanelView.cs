using Zenject;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(ScaleShakeAnim))]
public class MenuCoinPanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;
    private ScaleShakeAnim _shakeAnim;

    public void Awake()
    {
        _shakeAnim = GetComponent<ScaleShakeAnim>();
    }

    public void UpdateView(int coins)
    {
        _coinsText.text = coins.ToString();
        _shakeAnim.Play();
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

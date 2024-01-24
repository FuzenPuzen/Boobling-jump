using Zenject;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using EventBus;

public class SkinShopItemView : MonoBehaviour
{
	[SerializeField] private TMP_Text _cost;
	[SerializeField] private Button _buyButton;
	private PlayerSkinData _playerSkinData;

    private void Start()
    {
		_buyButton.onClick.AddListener(BuySkin);
    }

	private void BuySkin()
	{
		EventBus<OnTryBuySkin>.Raise(new() { playerSkinData = _playerSkinData });
	}

    public void SetValues(PlayerSkinData playerSkinData)
	{
        _playerSkinData = playerSkinData;       
    }

	public void UpdateView()
	{
        _cost.text = _playerSkinData.PlayerSkinSOData.Cost.ToString();
    }
}

public class SkinShopItemViewService : IService
{
	private IViewFabric _fabric;
	private SkinShopItemView _SkinShopItemView;
	private PlayerSkinView _playerSkinModel;
    private IMarkerService _markerService;
	private PlayerSkinData _playerSkinData;
    private EventBinding<OnBuySkin> _onBuySkin;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
		Transform parent = _markerService.GetTransformMarker<SkinShopPageMarker>().transform;
        _SkinShopItemView = _fabric.Init<SkinShopItemView>(parent);
        _onBuySkin = new EventBinding<OnBuySkin>(UpdateView);
    }
	public void UpdateView()
	{
        _SkinShopItemView.UpdateView();
    }

	public void SetData(PlayerSkinData playerSkinData)
	{
        _playerSkinData = playerSkinData;
        _SkinShopItemView.SetValues(_playerSkinData);
        _SkinShopItemView.UpdateView();
		Transform parent = _SkinShopItemView.transform;
        _playerSkinModel = _fabric.Init<PlayerSkinView>(_playerSkinData.PlayerSkinSOData.SkinPrefab, parent);
    }
}
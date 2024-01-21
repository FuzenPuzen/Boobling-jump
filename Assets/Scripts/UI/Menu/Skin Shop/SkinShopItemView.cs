using Zenject;
using UnityEngine;
using TMPro;

public class SkinShopItemView : MonoBehaviour
{
	[SerializeField] TMP_Text Cost;

	public void SetValues(PlayerSkinData playerSkinData)
	{
		Cost.text = playerSkinData.PlayerSkinSOData.Cost.ToString();
    }
}

public class SkinShopItemViewService : IService
{
	private IViewFabric _fabric;
	private SkinShopItemView _SkinShopItemView;
	private PlayerSkinView _playerSkinModel;
    private IMarkerService _markerService;
	private PlayerSkinData _playerSkinData;

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
    }

	public void SetData(PlayerSkinData playerSkinData)
	{
        _playerSkinData = playerSkinData;
        _SkinShopItemView.SetValues(_playerSkinData);
		Transform parent = _SkinShopItemView.transform;
        _playerSkinModel = _fabric.Init<PlayerSkinView>(_playerSkinData.PlayerSkinSOData.SkinPrefab, parent);
    }
}

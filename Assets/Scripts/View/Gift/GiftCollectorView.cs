using Zenject;
using UnityEngine;
using System;

public class GiftCollectorView : MonoBehaviour
{
	public Action collectAction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<GiftBoxView>(out GiftBoxView component))
		{
			collectAction?.Invoke();
        }
    }
}

public class GiftCollectorViewService : IService
{
	private GiftCollectorView _view;
	private IViewFabric _viewFabric;
	private IMarkerService _markerService;
    private IPoolsViewService _poolsViewService;
    private IPoolViewService _coinPoolViewService;

    [Inject]
	public void Constructor(IViewFabric viewFabric, IMarkerService markerService, IPoolsViewService poolsViewService)
	{
		_viewFabric = viewFabric;
		_markerService = markerService;
        _poolsViewService = poolsViewService;
    }

	public void ActivateService()
	{
        _coinPoolViewService = _poolsViewService.GetPool<DropedCoinViewService>();
        _view = _viewFabric.SpawnObject<GiftCollectorView>(_markerService.GetTransformMarker<PlayerMarker>().transform);
        _view.collectAction = GiftBoxCollected;
    }

	public void GiftBoxCollected()
	{
		//условия выбора спавна конкретного бонуса
		DropeCoinBonus();
		//другие бонусы
    }

	private void DropeCoinBonus()
	{
        _coinPoolViewService.GetItem().ActivateService();
        MonoBehaviour.print("GIFT:" + _coinPoolViewService.GetViewServicesCount());
	}

    private void DropeSuperJumpBonus()
    {

    }

    private void DropeRollBonus()
    {

    }
}

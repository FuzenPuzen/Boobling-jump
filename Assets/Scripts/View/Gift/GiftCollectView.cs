using Zenject;
using UnityEngine;
using System;

public class GiftCollectView : MonoBehaviour
{
	private Action _collectAction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<GiftBoxView>(out GiftBoxView component))
		{
			_collectAction?.Invoke();

        }
    }

    public void SetCollectAction(Action collectAction)
    {
        _collectAction = collectAction;
    }
}

public class GiftCollectViewService : IService
{
	private GiftCollectView _view;
	private IViewFabric _viewFabric;
	private IMarkerService _markerService;
	private IServiceFabric _serviceFabric;
	[Inject]
	public void Constructor(IViewFabric viewFabric, IMarkerService markerService, IServiceFabric serviceFabric)
	{
		_viewFabric = viewFabric;
		_markerService = markerService;
		_serviceFabric = serviceFabric;
	}

	public void ActivateService()
	{
        _view = _viewFabric.SpawnObject<GiftCollectView>(_markerService.GetTransformMarker<PlayerMarker>().transform);
        _view.SetCollectAction(GiftBoxCollected);
    }

	public void GiftBoxCollected()
	{
		//условия выбора спавна конкретного бонуса
		DropeCoinBonus();
		//другие бонусы
    }

	private void DropeCoinBonus()
	{
		_serviceFabric.Create<GiftCoinViewService>().ActivateService(_view.transform);
	}

    private void DropeSuperJumpBonus()
    {

    }

    private void DropeRollBonus()
    {

    }
}

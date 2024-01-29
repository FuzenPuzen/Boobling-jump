using Zenject;
using UnityEngine;
using System;

public class PlayerStoolDestroyerView : MonoBehaviour
{
	public Action<Collider> StoolCollideAction;

    private void OnTriggerEnter(Collider other)
    {
        StoolCollideAction.Invoke(other);
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

public class PlayerStoolDestroyerService : IService
{
	private IViewFabric _fabric;
	private PlayerStoolDestroyerView _PlayerStoolDestroyerView;
    private IMarkerService _markerService;
    private IPoolsViewService _poolsViewService;
    private IPoolViewService _coinPoolViewService;

    [Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService,
							 IPoolsViewService poolsViewService)
	{
        _poolsViewService = poolsViewService;
        _markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
        _coinPoolViewService = _poolsViewService.GetPool<DropedCoinViewService>();
        Transform parent = _markerService.GetTransformMarker<PlayerMarker>().transform;
        _PlayerStoolDestroyerView = _fabric.Init<PlayerStoolDestroyerView>(parent);
		_PlayerStoolDestroyerView.StoolCollideAction = OnStoolCollide;
        HideView();
    }

	public void OnStoolCollide(Collider other)
	{
        if (other.TryGetComponent(out BasicStoolView stoolView) && stoolView.CanSpawnCoin)
        {
            stoolView.DeActivateView();
            _coinPoolViewService.GetItem().ActivateService();
        }
    }

    public void HideView()
    {
        _PlayerStoolDestroyerView.HideView();
    }

    public void ShowView()
    {
        _PlayerStoolDestroyerView.ShowView();
    }
}

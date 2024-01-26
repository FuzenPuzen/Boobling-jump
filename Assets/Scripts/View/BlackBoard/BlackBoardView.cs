using Zenject;
using UnityEngine;
using EventBus;
using DG.Tweening;
using Unity.VisualScripting;

public class BlackBoardView : MonoBehaviour
{
	private DG.Tweening.Sequence _blackBoardMove;

	public void PlayerDieMove()
	{
		_blackBoardMove?.Kill();
        _blackBoardMove = DOTween.Sequence();
        _blackBoardMove.Append(transform.DOLocalMove(transform.position + Vector3.up * 5f, 0.1f));
        _blackBoardMove.Append(transform.DOLocalMove(new(-9.36f, 3.94f, -6.7f), 0.1f));
		_blackBoardMove.Join(transform.DOLocalRotate(new(5, -45f, -30f), 0.1f));
		_blackBoardMove.Join(transform.DOScale(Vector3.one * 1.7f, 0.1f));
    }
}

public class BlackBoardViewService : IService
{
	private IViewFabric _fabric;
	private BlackBoardView _blackBoardView;
    private IMarkerService _markerService;
	private EventBinding<OnPlayerDie> _onPlayerDie; 
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{
		Transform parent = _markerService.GetTransformMarker<BlackBoardSpawnMarker>().transform;
        _blackBoardView = _fabric.Init<BlackBoardView>(parent);
        _onPlayerDie = new(OnPlayerDie);
    }

	public void DeactivateService()
	{
        _onPlayerDie.Remove(OnPlayerDie);
    }

	private void OnPlayerDie()
	{
		_blackBoardView.PlayerDieMove();
	}
}

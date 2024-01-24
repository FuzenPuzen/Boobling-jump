using Zenject;
using UnityEngine;
using System.Collections;

public class RewardBonusTypeView : MonoBehaviour
{
	private float _changeTypeDelay;
	private IEnumerator _changeTypeDelayCor;
	public void ActivateView()
	{

	}

	public void ShowView()
	{

	}
	public void StopChangeType()
	{
		StopCoroutine(_changeTypeDelayCor);

    }
	private void ChangeType()
	{
		_changeTypeDelayCor = ChangeTypeDelayCor();
        StartCoroutine(_changeTypeDelayCor);
	}
	private IEnumerator ChangeTypeDelayCor()
	{
		yield return new WaitForSeconds(_changeTypeDelay);
		ChangeType();

    }
}

public class RewardBonusTypeViewService : IService
{
	private IViewFabric _fabric;
	private RewardBonusTypeView _rewardBonusTypeView;
    private IMarkerService _markerService;
	
	[Inject]
	public void Constructor(IViewFabric fabric, IMarkerService markerService)
	{
		_markerService = markerService;
		_fabric = fabric;
	}

	public void ActivateService()
	{       
        _rewardBonusTypeView = _fabric.Init<RewardBonusTypeView>();
	}
}

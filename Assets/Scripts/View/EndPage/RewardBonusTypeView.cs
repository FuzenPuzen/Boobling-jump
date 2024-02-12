using Zenject;
using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using System;

public class RewardBonusTypeView : MonoBehaviour
{
	[SerializeField] private TMP_Text _rewardBonusText;
	private float _changeTypeDelay = 0.25f;
	private IEnumerator _changeTypeDelayCor;
	private RewardBonusType _currentRewardBonusType;

	public void ActivateView()
	{
        ChangeType();
    }

	public void ShowView()
	{

	}

	public RewardBonusType StopChangeType()
	{
		StopCoroutine(_changeTypeDelayCor);
        return _currentRewardBonusType;
    }
	private void ChangeType()
	{
        _currentRewardBonusType = RandomEnumElement<RewardBonusType>.GetRandomEnumElement();
        _rewardBonusText.text = _currentRewardBonusType.ToString();
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
        Transform parent = _markerService.GetTransformMarker<RewardBonusTypePosMarker>().transform;
        _rewardBonusTypeView = _fabric.Init<RewardBonusTypeView>(parent);
        _rewardBonusTypeView.ActivateView();
	}

    public RewardBonusType StopChangeType()
	{
		return _rewardBonusTypeView.StopChangeType();

    }
}



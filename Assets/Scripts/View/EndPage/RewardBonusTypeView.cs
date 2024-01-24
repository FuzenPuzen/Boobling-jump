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

public enum RewardBonusType
{
    X2 = 2,
    X3 = 3,
    X5 = 5
}

public class RandomEnumElement<T> where T : Enum
{
    private static System.Random random = new System.Random();
    private static List<T> usedElements = new List<T>();

    public static T GetRandomEnumElement()
    {
        Array values = Enum.GetValues(typeof(T));

        List<T> unusedElements = new List<T>();
        foreach (T value in values)
        {
            if (!usedElements.Contains(value))
            {
                unusedElements.Add(value);
            }
        }

        if (unusedElements.Count == 0)
        {
            // Если все элементы использованы, очистите список и начните заново
            usedElements.Clear();
            unusedElements.AddRange((T[])values);
        }

        int randomIndex = random.Next(unusedElements.Count);
        T randomElement = unusedElements[randomIndex];
        usedElements.Add(randomElement);

        return randomElement;
    }
}


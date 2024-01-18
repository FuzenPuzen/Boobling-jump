using Zenject;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using EventBus;

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
    private IPoolViewService _roolBonusPoolViewService;
    private IPoolViewService _superJumpBonusPoolViewService;

    private DropMode _currentDropMode = DropMode.Full;

    private EventBinding<OnRollActivate> _onRollActivate;
    private EventBinding<OnRollDeactivate> _onRollDeactivate;
    private EventBinding<OnSupperJumpActivate> _onSupperJumpActivate;
    private EventBinding<OnSupperJumpDeactivate> _onSupperJumpDeactivate;

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
        _roolBonusPoolViewService = _poolsViewService.GetPool<DropedRollBonusViewService>();
        _superJumpBonusPoolViewService = _poolsViewService.GetPool<DropedSuperJumpBonusViewService>();
        _view = _viewFabric.SpawnObject<GiftCollectorView>(_markerService.GetTransformMarker<PlayerMarker>().transform);
        _view.collectAction = GiftBoxCollected;

        _onRollActivate = new(DeactivateFullDrop);
        _onSupperJumpActivate = new(DeactivateFullDrop);
        
        _onRollDeactivate = new(ActivateFullDrop);
        _onSupperJumpDeactivate = new(ActivateFullDrop);
    }

	public void GiftBoxCollected()
	{
        switch(_currentDropMode)
        {
            case DropMode.Full:
                MethodCaller methodCaller = new MethodCaller();
                methodCaller.AddMethod(DropeCoinBonus, 30);  // 30% ����������� ������ Method1
                methodCaller.AddMethod(DropeSuperJumpBonus, 40);  // 40% ����������� ������ Method2
                methodCaller.AddMethod(DropeRollBonus, 1000);  // 30% ����������� ������ Method3
                methodCaller.CallRandomMethod();
                break;

            case DropMode.OnlyCoins:
                DropeCoinBonus();
                break;

            case DropMode.Nothing:
                break;
        }

    }

    private void ActivateFullDrop() => _currentDropMode = DropMode.Nothing;
    private void DeactivateFullDrop() => _currentDropMode = DropMode.OnlyCoins;


	private void DropeCoinBonus()
	{
        _coinPoolViewService.GetItem().ActivateService();
	}

    private void DropeSuperJumpBonus()
    {
        _superJumpBonusPoolViewService.GetItem().ActivateService();
    }

    private void DropeRollBonus()
    {
        _roolBonusPoolViewService.GetItem().ActivateService();
    }

    public void DeactivateService()
    {
        _currentDropMode = DropMode.Nothing;
    }
}

public enum DropMode
{
    Full,
    Nothing,
    OnlyCoins
}

class MethodCaller
{
    private Dictionary<Action, int> methodProbabilities = new Dictionary<Action, int>();
    private System.Random random = new System.Random();

    // ���������� ������ � ��� �����������
    public void AddMethod(Action method, int probability)
    {
        methodProbabilities[method] = probability;
    }

    // ����� ���������� ������ � ������ �����������
    public void CallRandomMethod()
    {
        int totalProbability = methodProbabilities.Values.Sum();
        int randomNumber = random.Next(1, totalProbability + 1);

        foreach (var kvp in methodProbabilities)
        {
            if (randomNumber <= kvp.Value)
            {
                kvp.Key.Invoke();
                break;
            }
            randomNumber -= kvp.Value;
        }
    }
}

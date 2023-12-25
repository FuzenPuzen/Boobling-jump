using UnityEngine;
using Zenject;

public class GameState : IBaseState
{
    private StateMachine _statemachine;
    private IPlayerBehaviorService _playerBehaviorService;

    [Inject]
    public void Constructor(StateMachine statemachine,
                          IPlayerBehaviorService playerBehaviorService)
    {
        _statemachine = statemachine;
        _playerBehaviorService = playerBehaviorService;
    }

    public void Enter()
    {
        _playerBehaviorService.SetBehavior<PlayerSimpleJumpBehaviour>();
        _playerBehaviorService.SetActionEndBehavior(OnBehavourEnd);
    }

    public void Exit()
    {
        //do nothing
    }

    public void Update()
    {
        //do nothing
    }

    public void OnBehavourEnd()
    {
        _statemachine.SetState<SuperJumpState>();
    }

}

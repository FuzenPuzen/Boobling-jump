using UnityEngine;
using Zenject;

public class GameState : IBaseState
{
    private StateMachine _statemachine;
    private IPlayerBehaviorService _playerBehaviorService;
    private ISectionBehaviorService _sectionBehaviorService;

    [Inject]
    public void Constructor(StateMachine statemachine,
                          IPlayerBehaviorService playerBehaviorService,
                          ISectionBehaviorService sectionBehaviorService)
    {
        _sectionBehaviorService = sectionBehaviorService;
        _statemachine = statemachine;
        _playerBehaviorService = playerBehaviorService;
    }

    public void Enter()
    {
        _playerBehaviorService.SetBehavior<PlayerSimpleJumpBehaviour>();
        _sectionBehaviorService.SetBehavior<SectionSimpleJumpBehaviour>();
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

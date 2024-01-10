using System.Collections.Generic;
using System.Linq;
using Zenject;


public class MenuStateMachine 
{
    private IBaseState _currentState;

    private List<IBaseState> _baseStates = new();

    public void SetState<T>() where T : IBaseState
    {
        _currentState?.Exit();
        _currentState = _baseStates.OfType<T>().FirstOrDefault();
        _currentState.Enter();
    }

    [Inject]
    public void Constructor(MenuSkinShopPage menuSkinShopPage,
                            MenuStartState menuStartState,
                            MenuBonusPageState menuBonusPageState,
                            MenuMainPageState menuMainPageState)
    {
        _baseStates.Add(menuStartState);
        _baseStates.Add(menuSkinShopPage);
        _baseStates.Add(menuBonusPageState);
        _baseStates.Add(menuMainPageState);
    }

    public void UpdateState()
    {
        _currentState?.Update();
    }
}

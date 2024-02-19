using System;
using UnityEngine;

public class PlayerStartBehaviour : PlayerJumpBehaviour
{
    private PlayerStartBehaviourSOData _PlayerBehaviourData;
    private Action _startAction;


    public override Type GetBehaviourDataType()
    {
        return typeof(PlayerStartBehaviourSOData);
    }

    public override void SetBehaviourData(IPlayerBehaviourData playerBehaviourData)
    {
        _PlayerBehaviourData = (PlayerStartBehaviourSOData)playerBehaviourData;
    }

    public override void FallCallback()
    {
        _startAction?.Invoke();
    }

    public void SetStartAction(Action action)
    {
        _startAction = action;
    }

}

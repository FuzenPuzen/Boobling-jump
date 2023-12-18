using System;
using UnityEngine;
using Zenject;

public class PlayerSuperJumpBehavior : PlayerJumpBehavior
{
    private PlayerSuperJumpBehaviourSOData _PlayerBehaviorData = new();

    public PlayerSuperJumpBehavior(PlayerView playerView, float behaviorTime) : base(playerView, behaviorTime)
    {

    }


    public override void Fall()
    {
        base.Fall();
        //add force wave spawn
    }

    public override Type GetBehaviourDataType()
    {
        return _PlayerBehaviorData.GetType();
    }

    public override void SetBehaviourData(IPlayerBehaviourData playerBehaviourData)
    {
        _PlayerBehaviorData = (PlayerSuperJumpBehaviourSOData)playerBehaviourData;
    }

}

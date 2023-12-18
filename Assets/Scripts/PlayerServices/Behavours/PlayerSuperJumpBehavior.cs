using System;
using UnityEngine;
using Zenject;

public class PlayerSuperJumpBehavior : PlayerJumpBehavior
{
    private PlayerSuperJumpBehaviourSOData _PlayerBehaviorData;

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
        return typeof(PlayerSuperJumpBehaviourSOData);
    }

    public override void SetBehaviourData(IPlayerBehaviourData playerBehaviourData)
    {
        _PlayerBehaviorData = (PlayerSuperJumpBehaviourSOData)playerBehaviourData;
    }

}

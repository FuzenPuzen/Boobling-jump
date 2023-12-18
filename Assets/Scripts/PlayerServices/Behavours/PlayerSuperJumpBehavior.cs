using System;
using UnityEngine;

public class PlayerSuperJumpBehavior : PlayerJumpBehavior
{
    private PlayerSuperJumpBehaviourSOData _PlayerBehaviorData;

    public PlayerSuperJumpBehavior(PlayerView playerView) : base(playerView)
    {

    }


    public override void Fall()
    {
        base.Fall();
        //add force wave spawn
        MonoBehaviour.print("Super Jump Spawn");
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

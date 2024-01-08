using DG.Tweening;
using System;
using UnityEngine;

public class PlayerSuperJumpBehaviour : PlayerJumpBehaviour
{
    private PlayerSuperJumpBehaviourSOData _PlayerBehaviourData;

    public PlayerSuperJumpBehaviour(PlayerView playerView) : base(playerView)
    {

    }


    public override void Fall(TweenCallback tweenCallback)
    {
        base.Fall(FallCallback);
        //add force wave spawn
        MonoBehaviour.print("Super Jump Spawn");
    }

    public override Type GetBehaviourDataType()
    {
        return typeof(PlayerSuperJumpBehaviourSOData);
    }

    public override void SetBehaviourData(IPlayerBehaviourData playerBehaviourData)
    {
        _PlayerBehaviourData = (PlayerSuperJumpBehaviourSOData)playerBehaviourData;
    }

}

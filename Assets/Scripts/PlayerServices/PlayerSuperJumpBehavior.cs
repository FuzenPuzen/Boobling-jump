
using System.Diagnostics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerSuperJumpBehavior : PlayerJumpBehavior, IPlayerBehavior
{
    public PlayerSuperJumpBehavior(PlayerView playerView, float behaviorTime) : base(playerView, behaviorTime)
    {

    }

    public override void Fall()
    {
        base.Fall();
        //add force wave spawn
        MonoBehaviour.print("Force wave Spawn");
    }

}

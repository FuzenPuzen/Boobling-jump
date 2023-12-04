
using System.Diagnostics;
using UnityEngine;

public class PlayerSuperJumpBehavior : PlayerJumpBehavior, IPlayerBehavior
{
    public PlayerSuperJumpBehavior(PlayerView playerView) : base(playerView)
    {

    }

    public override void Fall()
    {
        base.Fall();
        //add force wave spawn
        MonoBehaviour.print("Force wave Spawn");
    }

}

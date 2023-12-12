using UnityEngine;
using Zenject;

public class PlayerSuperJumpBehavior : PlayerJumpBehavior
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

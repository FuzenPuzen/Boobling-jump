using System;

public class PlayerSimpleJumpBehaviour : PlayerJumpBehavior
{
    private PlayerSimpleBehaviourSOData _PlayerBehaviorData;
    public PlayerSimpleJumpBehaviour(PlayerView playerView) : base(playerView)
    {

    }

    public override Type GetBehaviourDataType()
    {
        return typeof(PlayerSimpleBehaviourSOData);
    }

    public override void SetBehaviourData(IPlayerBehaviourData playerBehaviourData)
    {
        _PlayerBehaviorData = (PlayerSimpleBehaviourSOData)playerBehaviourData;
    }
}

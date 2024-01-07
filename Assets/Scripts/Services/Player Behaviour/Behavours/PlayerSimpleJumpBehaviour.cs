using System;

public class PlayerSimpleJumpBehaviour : PlayerJumpBehaviour
{
    private PlayerSimpleBehaviourSOData _PlayerBehaviourData;
    public PlayerSimpleJumpBehaviour(PlayerView playerView) : base(playerView)
    {

    }

    public override Type GetBehaviourDataType()
    {
        return typeof(PlayerSimpleBehaviourSOData);
    }

    public override void SetBehaviourData(IPlayerBehaviourData playerBehaviourData)
    {
        _PlayerBehaviourData = (PlayerSimpleBehaviourSOData)playerBehaviourData;
    }
}

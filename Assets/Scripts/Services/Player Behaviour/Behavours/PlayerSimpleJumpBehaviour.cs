using System;

public class PlayerSimpleJumpBehaviour : PlayerJumpBehaviour
{
    private PlayerSimpleBehaviourSOData _PlayerBehaviourData;

    public override Type GetBehaviourDataType()
    {
        return typeof(PlayerSimpleBehaviourSOData);
    }

    public override void SetBehaviourData(IPlayerBehaviourData playerBehaviourData)
    {
        _PlayerBehaviourData = (PlayerSimpleBehaviourSOData)playerBehaviourData;
    }
}

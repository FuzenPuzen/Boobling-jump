using System;
using UnityEngine;

public interface IPlayerBehaviour 
{
    public void SetPlayerView(PlayerView playerView);
    public void UpdateBehaviour(); 
    public void StartBehaviour(); 
    public void StopBehaviour();

    public Type GetBehaviourDataType();
    public void SetBehaviourData(IPlayerBehaviourData playerBehaviourData);

    public void ColliderBehaviour(Collider other);
}

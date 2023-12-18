using System;
using UnityEngine;

public interface IPlayerBehavior 
{
    public void UpdateBehavior(); 
    public void StartBehavior(); 
    public void StopBehavior();

    public Type GetBehaviourDataType();
    public void SetBehaviourData(IPlayerBehaviourData playerBehaviourData);

    public void ColliderBehavior(Collider other);
}

using UnityEngine;

public interface IPlayerBehavior 
{
    public void UpdateBehavior(); 
    public void StartBehavior(); 
    public void StopBehavior();

    public void ColliderBehavior(Collider other);


}

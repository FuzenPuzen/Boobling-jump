using UnityEngine;
public interface IFabric 
{
    public T SpawnObjectAndGetType<T>(Vector3? position);
}

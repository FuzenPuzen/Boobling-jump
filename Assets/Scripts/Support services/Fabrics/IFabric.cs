using UnityEngine;
public interface IFabric 
{
    public T SpawnObjectAndGetType<T>(Vector3 position, Transform parent = null);
    public T SpawnObjectAndGetType<T>(Transform parent = null);
    public T SpawnObjectAndGetType<T>();
}

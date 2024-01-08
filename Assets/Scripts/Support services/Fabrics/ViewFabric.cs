using UnityEngine;
using Zenject;

public interface IViewFabric
{
    public T SpawnObject<T>(Vector3 position, Transform parent = null);
    public T SpawnObject<T>(Transform parent = null);
    public T SpawnObject<T>();
}

public class ViewFabric : IViewFabric
{
    private IPrefabStorageService _prefabsStorageService;

    [Inject]
    public ViewFabric(IPrefabStorageService prefabsStorageService)
    {
        _prefabsStorageService = prefabsStorageService;
    }

    public T SpawnObject<T>()
    {
        var obj = MonoBehaviour.Instantiate(_prefabsStorageService.GetPrefabByType<T>(),
                                    Vector3.zero,
                                    Quaternion.identity);
        return obj.GetComponent<T>();
    }

    public T SpawnObject<T>(Vector3 position, Transform parent = null)
    {
        var obj = MonoBehaviour.Instantiate(_prefabsStorageService.GetPrefabByType<T>(),
                                    position,
                                    Quaternion.identity, parent);
        return obj.GetComponent<T>();
    }

    public T SpawnObject<T>(Transform parent)
    {
        var obj = MonoBehaviour.Instantiate(_prefabsStorageService.GetPrefabByType<T>(), parent);
        return obj.GetComponent<T>();
    }
}



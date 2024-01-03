using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Fabric : IFabric
{
    private IPrefabStorageService _prefabsStorageService;

    [Inject]
    public Fabric(IPrefabStorageService prefabsStorageService)
    {
        _prefabsStorageService = prefabsStorageService;
    }

    public T SpawnObjectAndGetType<T>()
    {
        var obj = MonoBehaviour.Instantiate(_prefabsStorageService.GetPrefabByType<T>(),
                                    Vector3.zero,
                                    Quaternion.identity);
        return obj.GetComponent<T>();
    }

    public T SpawnObjectAndGetType<T>(Vector3 position, Transform parent = null)
    {
        var obj = MonoBehaviour.Instantiate(_prefabsStorageService.GetPrefabByType<T>(),
                                    position,
                                    Quaternion.identity, parent);
        return obj.GetComponent<T>();
    }

    public T SpawnObjectAndGetType<T>(Transform parent)
    {
        var obj = MonoBehaviour.Instantiate(_prefabsStorageService.GetPrefabByType<T>(), parent);
        return obj.GetComponent<T>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Fabric : IFabric
{
    private PrefabsStorageService _prefabsStorageService;

    [Inject]
    public Fabric(PrefabsStorageService prefabsStorageService)
    {
        _prefabsStorageService = prefabsStorageService;
    }

    public T SpawnObjectAndGetType<T>(Vector3? position = null)
    {
        if (position == null)
            position = Vector3.zero;
        var obj = MonoBehaviour.Instantiate(_prefabsStorageService.GetObjectByType<T>(),
                                    position.Value,
                                    Quaternion.identity);
        return obj.GetComponent<T>();
    }

    public T SpawnObjectAndGetType<T>()
    {
        var obj = MonoBehaviour.Instantiate(_prefabsStorageService.GetObjectByType<T>(),
                                    Vector3.zero,
                                    Quaternion.identity);
        return obj.GetComponent<T>();
    }
}

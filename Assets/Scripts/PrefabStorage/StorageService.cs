using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StorageService : IPrefabStorageService, ISOStorageService
{
    private List<GameObject> prefabs = new List<GameObject>();
    private List<ScriptableObject> ScriptableObjects = new List<ScriptableObject>();
    private string _prefabPath = "Prefabs/";
    private string _soPath = "SO/";

    public StorageService()
    {
        prefabs.AddRange(LoadPrefabsFromPath(_prefabPath));
        ScriptableObjects.AddRange(LoadSOFromPath(_soPath));
    }

    private GameObject[] LoadPrefabsFromPath(string path)
    {
        // Загружаем все объекты (префабы) в указанной директории
        GameObject[] objects = Resources.LoadAll<GameObject>(path);
        return objects;
    }

    private ScriptableObject[] LoadSOFromPath(string path)
    {
        // Загружаем все объекты (SO) в указанной директории
        ScriptableObject[] SOs = Resources.LoadAll<ScriptableObject>(path);
        return SOs;
    }


    public GameObject GetPrefabByType<T>()
    {
        GameObject obj = null;

        foreach (GameObject item in prefabs)
        {
            // Проверить, есть ли у игрового объекта компонент с заданным именем
            Component targetComponent = item.GetComponent(typeof(T).Name);

            if (targetComponent != null) obj = item;
        }

        if (obj == null) Debug.LogError($"Not found Prefabs with type {typeof(T).Name} in {_prefabPath}");
        return obj;
    }

    public ScriptableObject GetSOByType<T>()
    {
        ScriptableObject obj = null;
        foreach (ScriptableObject item in ScriptableObjects)
        {
            if (item.GetType() == typeof(T))
                obj = item;
        }
        if (obj == null) Debug.LogError($"Not found ScriptableObject with type {typeof(T).Name} in {_prefabPath}");
        return obj;
    }

    public List<ScriptableObject> GetSOsByType<T>()
    {
        List<ScriptableObject> objs = null;
        foreach (ScriptableObject item in ScriptableObjects)
        {
            if (item.GetType() == typeof(T))
                objs.Add(item);
        }
        if (objs == null) Debug.LogError($"Not found ScriptableObjects with type {typeof(T).Name} in {_prefabPath}");
        return objs;
    }

}

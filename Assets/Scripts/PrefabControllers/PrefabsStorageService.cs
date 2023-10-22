using System.Collections.Generic;
using UnityEngine;

public class PrefabsStorageService
{
    private List<GameObject> prefabs = new List<GameObject>();
    private string _assetPath = "Prefabs/";

    public PrefabsStorageService()
    {
        LoadPrefabsFromPath(_assetPath);
    }

    private void LoadPrefabsFromPath(string path)
    {
        // Загружаем все объекты (префабы) в указанной директории
        GameObject[] objects = Resources.LoadAll<GameObject>(path);
        prefabs.AddRange(objects);

        // Перебираем все подпапки в текущей директории
        string[] subDirectories = System.IO.Directory.GetDirectories("Assets/Resources/" + path);
        foreach (string subDir in subDirectories)
        {
            // Получаем имя подпапки
            string folderName = System.IO.Path.GetFileName(subDir);

            // Рекурсивно вызываем эту функцию для каждой подпапки
            LoadPrefabsFromPath(path + folderName + "/");
        }
    }

    public T GetPrefabByType<T>()
    {
        GameObject obj = null;

        foreach (GameObject item in prefabs)
        {
            // Проверить, есть ли у игрового объекта компонент с заданным именем
            Component targetComponent = item.GetComponent(typeof(T).Name);

            if (targetComponent != null) obj = item;
        }

        if (obj == null) Debug.LogError($"Not found Prefabs with type {typeof(T).Name} in {_assetPath}");
        return obj.GetComponent<T>();
    }

    public List<T> GetPrefabsByType<T>()
    {
        List<T> objs =  new List<T>();

        foreach (GameObject item in prefabs)
        {
            // Проверить, есть ли у игрового объекта компонент с заданным именем
            Component targetComponent = item.GetComponent(typeof(T).Name);

            if (targetComponent != null) objs.Add(item.GetComponent<T>());
        }

        return objs;
    }


}

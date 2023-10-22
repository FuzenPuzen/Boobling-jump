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
        // ��������� ��� ������� (�������) � ��������� ����������
        GameObject[] objects = Resources.LoadAll<GameObject>(path);
        prefabs.AddRange(objects);

        // ���������� ��� �������� � ������� ����������
        string[] subDirectories = System.IO.Directory.GetDirectories("Assets/Resources/" + path);
        foreach (string subDir in subDirectories)
        {
            // �������� ��� ��������
            string folderName = System.IO.Path.GetFileName(subDir);

            // ���������� �������� ��� ������� ��� ������ ��������
            LoadPrefabsFromPath(path + folderName + "/");
        }
    }

    public T GetPrefabByType<T>()
    {
        GameObject obj = null;

        foreach (GameObject item in prefabs)
        {
            // ���������, ���� �� � �������� ������� ��������� � �������� ������
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
            // ���������, ���� �� � �������� ������� ��������� � �������� ������
            Component targetComponent = item.GetComponent(typeof(T).Name);

            if (targetComponent != null) objs.Add(item.GetComponent<T>());
        }

        return objs;
    }


}

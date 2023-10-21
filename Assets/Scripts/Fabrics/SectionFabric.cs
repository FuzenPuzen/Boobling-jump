using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SectionFabric
{
    private PrefabsStorageService _prefabsStorageService;
    private List<GameObject> _sectionsPb = new();

    [Inject]
    public SectionFabric(PrefabsStorageService prefabsStorageService)
    {
        _prefabsStorageService = prefabsStorageService;
        _sectionsPb = _prefabsStorageService.GetSectionsPb();
    }

    public GameObject GetSpawnedSection()
    {
        int randId = Random.Range(0, _sectionsPb.Count);
        return MonoBehaviour.Instantiate(_sectionsPb[randId]);
    }
}

using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SectionFabric
{
    private PrefabsStorageService _prefabsStorageService;
    private List<SectionView> _sectionsPb = new();

    [Inject]
    public SectionFabric(PrefabsStorageService prefabsStorageService)
    {
        _prefabsStorageService = prefabsStorageService;
        _sectionsPb.AddRange(_prefabsStorageService.GetPrefabsByType<SectionView>());
    }

    public GameObject GetSpawnedSection()
    {
        int randId = Random.Range(0, _sectionsPb.Count);
        return MonoBehaviour.Instantiate(_sectionsPb[randId].gameObject);
    }
}

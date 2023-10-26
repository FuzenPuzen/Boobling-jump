using Zenject;
using UnityEngine;
using System.Collections.Generic;

public class StoolFabric: IStoolFabric
{
    private PrefabsStorageService _prefabsStorageService;
    private List<StoolView> _stoolsPb = new();

    [Inject]
    public StoolFabric(PrefabsStorageService prefabsStorageService)
    {
        _prefabsStorageService = prefabsStorageService;
        GetStoolPrefab();
    }

    private void GetStoolPrefab()
    {
        _stoolsPb = _prefabsStorageService.GetPrefabsByType<StoolView>();
    }


    public StoolService SpawnStool(int i)
    {
        StoolView stoolView = MonoBehaviour.Instantiate(_stoolsPb[i], new(30, 1.5f, 0), Quaternion.identity);
        StoolService stoolService = new(stoolView);
        return stoolService;
    }


}

using Zenject;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class StoolFabric: IStoolFabric
{
    private PrefabsStorageService _prefabsStorageService;
    private List<BasicStoolView> _stoolsPb = new();

    [Inject]
    public StoolFabric(PrefabsStorageService prefabsStorageService)
    {
        _prefabsStorageService = prefabsStorageService;
        GetStoolPrefab();
    }

    private void GetStoolPrefab()
    {
        _stoolsPb = _prefabsStorageService.GetPrefabsByType<BasicStoolView>();
    }


    public StoolService SpawnStool(int i)
    {
        var stoolView = MonoBehaviour.Instantiate(_stoolsPb[i], new(-10, 1.1f, 0), Quaternion.identity) as IStoolView;
        StoolService stoolService = new(stoolView);
        return stoolService;
    }


}

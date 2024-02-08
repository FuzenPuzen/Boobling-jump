using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IRepaintDataManager
{
    public RepaintSOData GetRandomRepaintSOData();
}

public class RepaintDataManager : IRepaintDataManager
{

    private ISOStorageService _storageService;
    private RepaintSODatas _repaintSODatas;
    
    [Inject]
    public void Constructor(ISOStorageService sOStorageService)
    {
        _storageService = sOStorageService;
        _repaintSODatas = (RepaintSODatas)_storageService.GetSOByType<RepaintSODatas>();
    }

    public RepaintSODatas GetRepaintSODataS() => _repaintSODatas;

    public RepaintSOData GetRandomRepaintSOData()
    {
        return _repaintSODatas.dictionary[Random.Range(0, _repaintSODatas.dictionary.Count)];
    }
}


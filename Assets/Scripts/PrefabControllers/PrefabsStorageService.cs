using System.Collections.Generic;
using UnityEngine;

public class PrefabsStorageService
{
    private List<GameObject> _playerSetPb = new();
    private const string PlayerAssetPath = "Prefabs/PlayerSet";

    private List<GameObject> _sectionsPb = new();
    private const string SectionsAssetPath = "Prefabs/Sections";


    public PrefabsStorageService()
    {
        _playerSetPb.AddRange(Resources.LoadAll<GameObject>(PlayerAssetPath));
        _sectionsPb.AddRange(Resources.LoadAll<GameObject>(SectionsAssetPath));
    }

    public GameObject GetPlayerPb() {  return _playerSetPb[0]; }

    public List<GameObject> GetSectionsPb() { return _sectionsPb; }

       
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class StoolPoolView : MonoBehaviour
{
    private List<GameObject> _busySections = new();
    private List<GameObject> _freeSections = new();
    public GameObject GetRandomSectionFromTier(int tierId)
    {
        CheckBusySections();
        SetFreeSections(tierId);       
        int sectionId = Random.Range(0, _freeSections.Count);
        var section = _freeSections[sectionId].gameObject;
        section.SetActive(true);

        _freeSections.Remove(section);
        _busySections.Add(section);
        return section;
    }

    private void SetFreeSections(int tierId)
    {
        var Tier = transform.GetChild(tierId);
        for (int i = 0; i < Tier.childCount; i++)
        {
            if (!_busySections.Contains(Tier.GetChild(i).gameObject) && !_freeSections.Contains(Tier.GetChild(i).gameObject))
            {
                _freeSections.Add(Tier.GetChild(i).gameObject);
            }           
        }
    }

    private void CheckBusySections()
    {
        if (_busySections.Count == 0)
            return;
        var temlist = _busySections.ToList();
        foreach (GameObject section in temlist)
        {
            if (CheckActiveChilds(section))
            {
                SetSectionToStartState(section);
            }
        }
    }

    private bool CheckActiveChilds(GameObject section)
    {
        for (int i = 0; i < section.transform.childCount; i++)
        {
            if (section.transform.GetChild(i).gameObject.activeSelf) return false;
        }
        return true;
    }

    private void SetSectionToStartState(GameObject section)
    {
        _busySections.Remove(section);
        for (int i = 0; i < section.transform.childCount; i++)
        {
            section.transform.GetChild(i).gameObject.SetActive(true);
        }
        section.SetActive(false);
    }
}

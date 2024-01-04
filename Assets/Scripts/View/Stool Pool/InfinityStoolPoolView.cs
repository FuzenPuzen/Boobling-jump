using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InfinityStoolPoolView : MonoBehaviour
{
    private List<GameObject> _busySections = new();
    private List<GameObject> _freeSections = new();
    public SectionView GetSection()
    {
        SetFreeSections();
        int sectionId = Random.Range(0, _freeSections.Count);
        var section = _freeSections[sectionId].gameObject;
        section.SetActive(true);
        _freeSections.Remove(section);
        _busySections.Add(section);
        return section.GetComponent<SectionView>();
    }

    private void SetFreeSections()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!_busySections.Contains(transform.GetChild(i).gameObject) && !_freeSections.Contains(transform.GetChild(i).gameObject))
            {
                _freeSections.Add(transform.GetChild(i).gameObject);
            }
        }
    }

    public void ReturnSection(SectionView sectionView)
    {
        GameObject temp = sectionView.GetComponent<GameObject>();
        _busySections.Remove(temp);
    }
}

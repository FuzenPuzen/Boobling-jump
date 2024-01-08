using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoolPoolView : MonoBehaviour, IstoolPoolView
{
    private List<GameObject> _busySections = new();
    private List<GameObject> _freeSections = new();
    public virtual SectionView GetSection()
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
        GameObject temp = sectionView.GameObject();
        temp.transform.localPosition = Vector3.zero;
        temp.SetActive(false);
        _busySections.Remove(temp);
    }
}

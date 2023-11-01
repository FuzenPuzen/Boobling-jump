using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiersView : MonoBehaviour
{
    public GameObject GetRandomSectionFromTier(int tierId)
    {
        int sectionId = Random.Range(0, transform.GetChild(tierId).childCount);
        return transform.GetChild(tierId).transform.GetChild(sectionId).gameObject;
    }

}

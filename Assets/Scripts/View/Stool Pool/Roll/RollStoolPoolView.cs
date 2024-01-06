using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RollStoolPoolView : MonoBehaviour
{
    private int _currectSection = 0;

    public SectionView GetSection() => transform.GetChild(_currectSection++).GetComponent<SectionView>();

}

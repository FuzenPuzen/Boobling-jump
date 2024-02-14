using UnityEngine;

public class StoolArranger : MonoBehaviour
{
    public float spacing = 5.0f;
    private int _stoolcount = 0;


    [ContextMenu("ArrangeChildren")]
    void ArrangeChildren()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {            
            Transform child = transform.GetChild(i);
            if (child.GetComponent<BasicStoolView>())
            {
                _stoolcount++;
                child.localPosition = new Vector3(i * spacing, 0, 0);
            }
        }
        transform.GetComponent<BoxCollider>().center = new Vector3(_stoolcount * spacing, 0, 0);
        _stoolcount = 0;
    }
}

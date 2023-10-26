using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoolView : MonoBehaviour, IStoolView
{
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, -Vector3.right * 10, 7 * Time.deltaTime);
    }

    public void DestroyStool()
    {
        StartCoroutine(DestroyView());
    }

    private IEnumerator DestroyView()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}

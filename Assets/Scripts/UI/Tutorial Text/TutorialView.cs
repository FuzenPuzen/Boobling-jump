using System.Collections;
using UnityEngine;

public class TutorialView : MonoBehaviour
{

    public void StartDestoroyTimer()
    {
        StartCoroutine(DestroyCD());
    }

    private IEnumerator DestroyCD()
    {
        yield return new WaitForSecondsRealtime(2f);
        Destroy(gameObject);
    }

}

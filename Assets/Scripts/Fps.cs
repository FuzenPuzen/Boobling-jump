using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fps : MonoBehaviour
{
    private float count;
    public Text FpsText;

    private IEnumerator Start()
    {
        GUI.depth = 2;
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            FpsText.text = count.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }

}
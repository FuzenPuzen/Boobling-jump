using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowChekers : MonoBehaviour
{
    public void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
            Debug.Log("OnTriggerExit");
        if (other.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
            Debug.Log(other.tag);
        Debug.Log("OnTriggerEner");
        if (other.CompareTag("Ground"))
        {
            gameObject.SetActive(true);
        }
    }
}

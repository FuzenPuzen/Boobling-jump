using UnityEngine;
using EventBus;

public class FinishLineView : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>())
        {
            EventBus<OnTutorialFinish>.Raise();
        }
    }
}

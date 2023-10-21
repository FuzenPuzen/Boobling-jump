using Unity.Mathematics;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public Transform player;

    public float minSize = 0.05f; // Минимальный размер объекта
    public float maxSize = 1.0f; // Максимальный размер объекта
    public float maxDistance = 4.0f;

    public GameObject firstrigger;
    public GameObject secondtrigger;

    void Update()
    {
        transform.position = new(player.position.x,transform.position.y,transform.position.z);

        float distance = Mathf.Abs(transform.position.y - player.position.y);
        float t = Mathf.Clamp01(distance / maxDistance);

        // Интерполируем размер объекта между minSize и maxSize в зависимости от расстояния
        float newSize = Mathf.Lerp(minSize, maxSize, 1 - t); // Инвертируем t здесь

        // Применяем новый размер к объекту
        transform.localScale = new Vector3(newSize, 0.1f, newSize);

        bool first = firstrigger.activeSelf;
        bool second = secondtrigger.activeSelf;
        if (first && second)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}

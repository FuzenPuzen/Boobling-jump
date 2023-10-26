using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public Transform player;

    public float minSize = 0.05f; // ����������� ������ �������
    public float maxSize = 1.0f; // ������������ ������ �������
    public float maxDistance = 10.0f;


    void Update()
    {
        transform.position = new(player.position.x,transform.position.y,transform.position.z);

        float distance = Mathf.Abs(transform.position.y - player.position.y);
        float t = Mathf.Clamp01(distance / maxDistance);

        // ������������� ������ ������� ����� minSize � maxSize � ����������� �� ����������
        float newSize = Mathf.Lerp(minSize, maxSize, 1 - t); // ����������� t �����

        // ��������� ����� ������ � �������
        transform.localScale = new Vector3(newSize, 0.1f, newSize);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotateController : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // ������������� �������� ��������

    void Update()
    {
        // �������� ������� ���� �������� ������� ������ ��� Y
        float currentRotation = transform.eulerAngles.y;

        // ��������� ����� ���� ��������
        float newRotation = currentRotation + rotationSpeed * Time.deltaTime;

        // ��������� ����� ���� �������� � �������
        transform.rotation = Quaternion.Euler(0, newRotation, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotateController : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Устанавливаем скорость вращения

    void Update()
    {
        // Получаем текущий угол вращения объекта вокруг оси Y
        float currentRotation = transform.eulerAngles.y;

        // Вычисляем новый угол вращения
        float newRotation = currentRotation + rotationSpeed * Time.deltaTime;

        // Применяем новый угол вращения к объекту
        transform.rotation = Quaternion.Euler(0, newRotation, 0);
    }
}

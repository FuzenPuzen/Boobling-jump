using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnim : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateVector;
    [SerializeField] private float _rotateSpeed = 1;

    public void Rotate()
    {
        transform.Rotate(_rotateVector * _rotateSpeed);
    }

    public void FixedUpdate()
    {
        Rotate();
    }
}

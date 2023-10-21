using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float jumpAngle = 25f; // Угол прыжка в градусах
    public float fallSpeed = 30f;

    public GameObject playerModel;

    public event Action ReachPlatform;
    private GameObject _currentPlanform;

    public bool canJump = true;
    private Rigidbody rb;

    [Inject] private DiContainer _container;

    [Inject]
    public void Construct()
    {
        _container.Bind<PlayerController>().ToSelf();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
    }



    private void HandleInput()
    {
        // Проверка нажатия левой кнопки мыши для резкого падения
        if (Input.GetMouseButtonDown(0))
        {
            ApplyFallForce();
        }
    }

    private void ApplyFallForce()
    {
        rb.velocity = new(rb.velocity.x, -fallSpeed, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            HandleGroundCollision(collision);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            HandleGroundCollision(collision);
           // HandleEnemyCollision();
        }
    }

    private void HandleGroundCollision(Collision collision)
    {
        if (canJump)
        {
            Jump();
        }
        if (_currentPlanform != collision.gameObject)
        {
            ReachPlatform?.Invoke();
            _currentPlanform = collision.gameObject;
        }
    }

    private void HandleEnemyCollision()
    {
        SceneManager.LoadScene(0);
    }

    private void Jump()
    {
        canJump = false;
        float jumpAngleInRadians = jumpAngle * Mathf.Deg2Rad;
        Vector3 jumpDirection = new(Mathf.Sin(jumpAngleInRadians), Mathf.Cos(jumpAngleInRadians), 0);
        Vector3 jumpForceVector = jumpDirection * jumpForce;

        rb.velocity = jumpForceVector;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : StaticInstance<Player>
{
    // Settings
    public float acceleration = 50f;
    public float maxSpeed = 15f;
    public float dragCoefficient = 0.98f;
    public float steeringAngle = 20f;
    public float traction = 1f;
    public bool debug = false;
    public bool canTakeDamage = true;
    [SerializeField] private int _score = 0;

    private int health = 3;

    // Input and state variables
    private float accelerationInput = 1f;
    private float steeringInput = 1f;
    private float steeringDirection = 0.5f;
    public Rigidbody rb;

    
    [SerializeField] MeshRenderer _meshRenderer;
    private Material _material;


    // Movement variables
    private Vector3 velocity;


    void UpdateInput()
    {
        if (debug)
        {
            steeringInput = Input.GetAxis("Horizontal");
            accelerationInput = Input.GetAxis("Vertical");
        }
        else
        {
            accelerationInput = 1;
            // Steering logic
            if (Input.anyKeyDown)
            {
                steeringDirection *= -1f;
            }

            if (Input.anyKey)
            {
                steeringInput = steeringDirection;
            }
            else
            {
                steeringInput = 0f;
            }
        }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _material = _meshRenderer.material;
    }

    void Update()
    {
        UpdateInput();
        Debug.DrawRay(transform.position, velocity.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
    }

    void FixedUpdate()
    {
        // Accelerate the car
        velocity += transform.forward * acceleration * Time.deltaTime * accelerationInput;
        transform.position += velocity * Time.deltaTime;
        transform.Rotate(Vector3.up * steeringInput * velocity.magnitude * steeringAngle *
                         Time.deltaTime);

        // Apply drag and limit max speed
        velocity *= dragCoefficient;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Apply traction
        velocity =
            Vector3.Lerp(velocity.normalized, transform.forward,
                traction * Time.deltaTime) * velocity.magnitude;
    }

    public void TakeDamage()
    {
        if (canTakeDamage == false) return;
        health--;
        canTakeDamage = false;

        Debug.Log("Health: " + health);
        HealthUi.Instance.SetHealth(health);
        
        // Flash the player red 3 times over 1 second
        DOTween.Sequence()
            .Append(_material.DOColor(Color.red, 0.1f))
            .Append(_material.DOColor(Color.white, 0.1f))
            .Append(_material.DOColor(Color.red, 0.1f))
            .Append(_material.DOColor(Color.white, 0.1f))
            .Append(_material.DOColor(Color.red, 0.1f))
            .Append(_material.DOColor(Color.white, 0.1f))
            .OnComplete(() => canTakeDamage = true);
        
        if (health <= 0)
        {
            Die();
        }

 
    }

    public void Die()
    {
        Debug.Log("You died!");
        // Show game over screen
        GameOverPanel.Instance.TriggerGameOver();
    }



    public void AddScore()
    {
        _score++;
        Debug.Log("Score: " + _score);
        ScoreTxt.Instance.SetScore(_score*10);
    }
}
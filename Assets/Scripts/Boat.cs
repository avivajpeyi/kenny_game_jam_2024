using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boat : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float minSpeed = 1.0f; // Minimum speed of the object
    public float maxSpeed = 5.0f; // Maximum speed of the object
    public float errorMargin = 0.3f; // Error margin for the random error

    [SerializeField]
    private float currentDistance; // Serialized variable to track the current distance from (0,0,0)

    private float speed;
    private Vector3 direction;
    private Vector3 x0 = Vector3.zero;
    

    private void Start()
    {
        
        player = Player.Instance.transform; // Get the player's Transform
        // Initialize speed with a random value between minSpeed and maxSpeed
        speed = Random.Range(minSpeed, maxSpeed);

        transform.position = new Vector3(
            transform.position.x, player.position.y, transform.position.z);
        x0 = transform.position; // Set the initial position as the origin

        
        // Initialize direction towards the player with some random error
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer += new Vector3(
            Random.Range(-errorMargin, errorMargin), 0, 0
        );
        directionToPlayer.Normalize(); // Normalize to maintain consistent direction
        directionToPlayer.y = 0; // Ensure the object moves only in the XZ plane

        direction = directionToPlayer;
    }

    private void Update()
    {
        // Move the object in the direction
        transform.position += direction * speed * Time.deltaTime;

        // Update current distance from origin
        currentDistance = Vector3.Distance(Vector3.zero, transform.position);

        // Check if the object has traveled too far from (0,0,0)
        if (currentDistance > DespawnCircle.radius)
        {
            Destroy(gameObject); // Delete the object
        }

        // Make the object face the direction of motion
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log( gameObject.name + " collided with " + collision.gameObject
        .name );
        
        if (collision.gameObject.CompareTag("WreckingBall"))
        {
            Destroy(gameObject);
            Player.Instance.AddScore();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Player.Instance.TakeDamage();
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a line from start to the direction + 50
        Gizmos.color = Color.red;
        Gizmos.DrawLine(x0, x0 + direction * 50);
    }
}

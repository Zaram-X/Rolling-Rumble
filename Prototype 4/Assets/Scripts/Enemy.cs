using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Speed at which the enemy moves towards the player
    public float speed = 3.0f;

    // Reference to the Rigidbody component for physics-based movement
    private Rigidbody enemyRb;
    
    // Reference to the player object that the enemy will follow
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the enemy for movement
        enemyRb = GetComponent<Rigidbody>();

        // Find and store the player object in the scene
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction from the enemy to the player, normalize it to keep the speed consistent
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        // Apply force to the enemy to move it towards the player
        enemyRb.AddForce(lookDirection * speed);  

        // If the enemy falls below a certain height, destroy it
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}

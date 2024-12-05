using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Speed at which the player moves forward
    public float speed = 5.0f;

    // Strength of the powerup (how much force it applies to enemies)
    private float powerupStrenght = 15.0f;

    // Reference to the Rigidbody component of the player for physics-based movement
    private Rigidbody playerRb;

    // Reference to the focal point (camera reference to move according to the camera's direction)
    private GameObject focalPoint;

    // Indicator for the powerup effect
    public GameObject poweupIndicator;

    // Whether the player currently has a powerup
    public bool hasPowerup = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the player
        playerRb = GetComponent<Rigidbody>();

        // Find the object named "Focal Point" in the scene to move the player based on camera direction
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        // Get vertical input from the user (forward/backward movement)
        float fowardInput = Input.GetAxis("Vertical"); // Value ranges from -1 to 1

        // Move the player based on the focal point's direction and user input
        playerRb.AddForce(focalPoint.transform.forward * speed * fowardInput);

        // Make the powerup indicator follow the player (slightly below the player)
        poweupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    // Triggered when the player collides with a powerup object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            // Activate powerup and show the powerup indicator
            hasPowerup = true;
            poweupIndicator.gameObject.SetActive(true);

            // Destroy the powerup object that was collected
            Destroy(other.gameObject);

            // Start the countdown for the powerup duration
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    // Coroutine to handle the powerup countdown (7 seconds)
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);

        // Deactivate powerup after the countdown ends
        hasPowerup = false;
        poweupIndicator.gameObject.SetActive(false);
    }

    // Triggered when the player collides with an enemy object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            // Get the Rigidbody of the enemy to apply force
            Rigidbody enemyRigibody = collision.gameObject.GetComponent<Rigidbody>();

            // Calculate the direction away from the player to apply force
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            // Apply force to the enemy if the player has a powerup
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            enemyRigibody.AddForce(awayFromPlayer * powerupStrenght, ForceMode.Impulse);
        }
    }
}

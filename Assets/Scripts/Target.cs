using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;
    public int pointValue; // Each object has its own point value

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>(); // Retrieve the current object's rigidbody
        // Find the object named "Game Manager" in the Hierarchy and get its "GameManager" script
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive) // If the game is currently active
        {
            Destroy(gameObject); // When the player presses the left mouse button on a target, the target will be destroyed
            gameManager.UpdateScore(pointValue); // Add points to the score whenever an object is clicked on
            // Create an explosion particle at the current game object's position
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject); // When a target reaches the sensor's collider, the target will be destroyed

        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver(); // Generate the game over text if any good objects hit the sensor
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed); // Add an upward force of a random amplitude between 12 and 16 inclusive
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos); // z will always be 0
    }
}

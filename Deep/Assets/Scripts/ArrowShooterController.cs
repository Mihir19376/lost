using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooterController : MonoBehaviour
{
    // This is the arrow that this arrow shooter will Instantiate. 
    public GameObject arrowPrefab;
    // The time left between a shot
    private float timeBetweenShot;
    // This is the mimimum time between shots.
    private float startTimeBetweenShot = 2f;
    // This is the distance wat from the shooter it shoots. 
    private float distanceSpawn = .2f;
    // Start is called before the first frame update
    void Start()
    {
        // Set the the time left to the min time. 
        timeBetweenShot = startTimeBetweenShot;
    }

    // Update is called once per frame
    void Update()
    {
        // If the wat time has lasped, and the current time between shots is 0 
        if (timeBetweenShot <= 0)
        {
            // Calculate the spawn position for the arrow relative to the shooter positon. 
            Vector2 arrowSpawnPos = new Vector2(transform.position.x + distanceSpawn * -transform.localScale.y, transform.position.y);
            // Instantiate a new arrow game object at the calculated spawn position with the same rotation of the shooter. 
            GameObject spawnedArrow = Instantiate(arrowPrefab, arrowSpawnPos, transform.rotation);
            // Get a reference to the ArrowMoveController script attached to the spawned arrow.
            ArrowMoveController arrowMoveController = spawnedArrow.GetComponent<ArrowMoveController>();

            // Check the direction of the arrow shooter.
            if (transform.localScale.y > 0)
            {
                // If facing right, set the arrow's direction to go left.
                arrowMoveController.directionMultiplier = -1;
            }
            else
            {
                // If facing left, set the arrow's direction to go right.
                arrowMoveController.directionMultiplier = 1;
            }
            // Reset the timeBetweenShot to its initial value.
            timeBetweenShot = startTimeBetweenShot;
        }
        else
        {
            // If timeBetweenShot has not elapsed, decrease it by the time passed since the last frame.
            timeBetweenShot -= Time.deltaTime;
        }
    }
}

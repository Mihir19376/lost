using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooterController : MonoBehaviour
{
    public GameObject arrowPrefab;
    private float timeBetweenShot;
    private float startTimeBetweenShot = 2f;
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenShot = startTimeBetweenShot;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenShot <= 0)
        {
            Vector2 arrowSpawnPos = new Vector2(transform.position.x + .2f * -transform.localScale.y, transform.position.y);
            GameObject spawnedArrow = Instantiate(arrowPrefab, arrowSpawnPos, transform.rotation);
            ArrowMoveController arrowMoveController = spawnedArrow.GetComponent<ArrowMoveController>();
            if (transform.localScale.y > 0)
            {
                // sets the arrow to go left.
                arrowMoveController.directionMultiplier = -1;
            }
            else
            {
                // sets the arrow to go right.
                arrowMoveController.directionMultiplier = 1;
            }
            timeBetweenShot = startTimeBetweenShot;
        }
        else
        {
            timeBetweenShot -= Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveScript : MonoBehaviour
{
    public float moveSpeed = 3f;  // Speed at which the platform moves
    Vector2 leftPoint;   // Leftmost point of movement
    Vector2 rightPoint;  // Rightmost point of movement
    public float rightDistance = 5; // distance that it travels.
    private bool movingRight = true; // boolean to denote what dierection its travelling.

    private void Start()
    {
        // The left point is its starting point
        leftPoint = transform.position;
        // The right point is a set distace away. 
        rightPoint = new Vector2(transform.position.x + rightDistance, transform.position.y);
        // The move speed of the object, randomized between 1 and 2.
        moveSpeed = Random.Range(1, 2);
    }

    private void Update()
    {

        // Check if the object's current position is very close to the left movement limit.
        if (Vector2.Distance(transform.position, leftPoint) < 0.02f)
        {
            // Set a flag indicating that the object is moving to the right.
            movingRight = true;
            
        }
        // Check if the object's current position is very close to the right movement limit.
        else if (Vector2.Distance(transform.position, rightPoint) < 0.02f)
        {
            // Set a flag indicating that the object is moving to the left.
            movingRight = false;
            
        }

        // Move the object based on its current movement direction.
        if (movingRight)
        {
            // Move the object towards the right movement limit.
            transform.position = Vector2.MoveTowards(transform.position, rightPoint, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Move the object towards the left movement limit.
            transform.position = Vector2.MoveTowards(transform.position, leftPoint, moveSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Handle the parenting of other objects upon collision.
    /// </summary>
    /// <param name="collision">The collision data.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player's position is below the collision object's position.
        if (transform.position.y < collision.transform.position.y)
        {
            // Parent the collision object to the player.
            collision.transform.SetParent(transform);
        }
        
    }

    /// <summary>
    /// Handle unparenting of other objects upon collision exit.
    /// </summary>
    /// <param name="collision">The collision data.</param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Unparent the collision object from the player.
        collision.transform.SetParent(null);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(rightPoint, .1f);
    }

}
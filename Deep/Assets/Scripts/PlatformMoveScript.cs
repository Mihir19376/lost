using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveScript : MonoBehaviour
{
    public float moveSpeed = 3f;  // Speed at which the platform moves
    Vector2 leftPoint;   // Leftmost point of movement
    Vector2 rightPoint;  // Rightmost point of movement
    public float leftDistance = 5;
    public float rightDistance = 5;
    private bool movingRight = true;

    private void Start()
    {
        leftPoint = new Vector2(transform.position.x - leftDistance, transform.position.y);
        rightPoint = new Vector2(transform.position.x + rightDistance, transform.position.y);
    }

    private void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, rightPoint, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, leftPoint) < 0.02f)
        {
            movingRight = true;
            
        }
        else if (Vector2.Distance(transform.position, rightPoint) < 0.02f)
        {
            movingRight = false;
            
        }

        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, rightPoint, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, leftPoint, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.position.y < collision.transform.position.y)
        {
            collision.transform.SetParent(transform);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }

}
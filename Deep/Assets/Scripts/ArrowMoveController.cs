using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMoveController : MonoBehaviour
{
    public Rigidbody2D arrowRb; // Reference to the Rigidbody2D component of the arrow.
    public float directionMultiplier; // Controls the arrow's direction (1 for right, -1 for left).
    public GameObject player; // Reference to the player game object.

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component of the arrow.
        arrowRb = gameObject.GetComponent<Rigidbody2D>();
        // Find the player using its tag.
        player = GameObject.FindGameObjectWithTag("playerTag");
    }

    // Update is called once per frame
    void Update()
    {
        // Set the arrow's velocity to move horizontally based on directionMultiplier.
        arrowRb.velocity = new Vector2(5*directionMultiplier, 0f);
    }

    /// <summary>
    /// called when a collision occurs. 
    /// </summary>
    /// <param name="collision">the collision data.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged as "playerTag." (the player).
        if (collision.gameObject.tag == "playerTag")
        {
            // Call the TakePlayerDamage method on the player's PlayerMovement2D script, causing the player to take damage.
            player.GetComponent<PlayerMovement2D>().TakePlayerDamage(1);
        }
        // Destroy the arrow game object upon collision with any object.
        Destroy(gameObject);
    }
}

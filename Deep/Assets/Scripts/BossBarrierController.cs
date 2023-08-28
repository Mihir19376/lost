using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarrierController : MonoBehaviour
{
    // Reference to a warning message game object.
    public GameObject warningMessage;
    // Boolean indicating whether a warning wait is active.
    private bool warningWait;
    // Reference to a particle effect game object.
    public GameObject particleEffect;
    // Reference to the boss game object.
    public GameObject boss;
    // The number of gems needed to pass thsi barrier and eneter the boss level. 
    private int neededGems = 30;
    // How long the warning is displayed for
    int warningDuration = 1;

    // Start is called before the first frame update
    void Start()
    {
        warningWait = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Handle collision with another object.
    /// </summary>
    /// <param name="collision">The collision data.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has the "playerTag."
        if (collision.gameObject.tag == "playerTag")
        {
            // Check if the player has enough gems to meet the neededGems requirement.
            if (collision.gameObject.GetComponent<PlayerMovement2D>().gems >= neededGems)
            {
                // Instantiate a particle effect at the player's position.
                Instantiate(particleEffect, collision.transform.position, Quaternion.identity);
                // Set the bossLevelInitiated flag in the BossController to true.
                boss.GetComponent<BossController>().bossLevelInitiated = true;
                // Destroy this barrier.
                Destroy(gameObject);
            }
            else
            {
                // Set the warningWait flag to false.
                warningWait = false;
                // Start a coroutine to show a warning message.
                StartCoroutine(showMessage());
            }
            
        }
    }

    /// <summary>
    /// Coroutine to display a warning message.
    /// </summary>
    IEnumerator showMessage()
    {
        // Check if the warning is not already being displayed.
        if (!warningWait)
        {
            // Activate the warning message game object.
            warningMessage.SetActive(true);
            // Wait for 1 second before deactivating the warning message.
            yield return new WaitForSeconds(warningDuration);
            // Deactivate the warning message.
            warningMessage.SetActive(false);
            // Set the warningWait flag to true to prevent further messages.
            warningWait = true;
        }
    }
}

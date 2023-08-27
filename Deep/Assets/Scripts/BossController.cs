using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    // Indicates whether the boss level has been initiated.
    public bool bossLevelInitiated;
    // The layer mask for detecting the player.
    public LayerMask playerLayer;
    // Reference to the player's position.
    private Transform playerPos;
    // The speed at which the boss moves.
    public float bossSpeed = .2f;
    // Reference to the boss's animator.
    public Animator bossAnimator2D;
    // The time left betweeen boss attacks.
    private float timeBetweenAttack;
    // The initial time between boss attacks.
    private float startTimeBetweenAttack = .3f;
    // Reference to the player object.
    public GameObject player;
    // The health of the boss.
    public int bossHealth;
    // Reference to the slider used to represent boss health.
    public Slider slider;
    // Reference to the portal game object—used to win the game.
    public GameObject portal;
    // Reference to a particle effect game object.
    public GameObject particleEffect;
    // The maximum boss health
    public int maxBossHealth = 30;
    // The radius at which the boss checks for the player. 
    private float checkRadius = .3f;
    // Start is called before the first frame update
    void Start()
    {
        // denote that the boss level hast started yet when the game begins. 
        bossLevelInitiated = false;
        // set the player object of the object in the scene that has the tag "playerTag"
        player = GameObject.FindGameObjectWithTag("playerTag");
        // set the beggining boss health to 30.
        bossHealth = maxBossHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Call a function to handle the health bar, updating the boss's health display.
        HandleHealthBar();
        // Check if the boss level has been initiated (by the player entering through the barrier).
        if (bossLevelInitiated)
        {
            // Check if the boss is within a certain radius of the player using Physics2D.OverlapCircle.
            if (Physics2D.OverlapCircle(transform.position, checkRadius, playerLayer))
            {
                // Set the boss's animator parameters to indicate the attack state.
                bossAnimator2D.SetBool("bossAttacking", true);
                bossAnimator2D.SetBool("bossRunning", false);

                // Check if it's time for the boss to attack based on the timeBetweenAttack.
                if (timeBetweenAttack <= 0)
                {
                    // Deal damage to the player by calling the TakePlayerDamage method on the player object.
                    player.GetComponent<PlayerMovement2D>().TakePlayerDamage(1);
                    // Reset the timeBetweenAttack to the initial value.
                    timeBetweenAttack = startTimeBetweenAttack;
                }
                else
                {
                    // Decrease the timeBetweenAttack timer by deltaTime to control the attack rate.
                    timeBetweenAttack -= Time.deltaTime;
                }
            }
            else
            {
                // Move towards the player.
                // Find the player's position based on the object in the scenw with the "playerTag" tag.
                playerPos = GameObject.FindGameObjectWithTag("playerTag").GetComponent<Transform>();
                // Calculate the target position, maintaining the same Y-coordinate as the boss.
                Vector2 target = new Vector2(playerPos.position.x, transform.position.y);
                // Move the boss towards the target position using MoveTowards.
                transform.position = Vector2.MoveTowards(transform.position, target, bossSpeed * Time.deltaTime);
                // Set animator parameters to indicate that the boss is back to moving.
                bossAnimator2D.SetBool("bossAttacking", false);
                bossAnimator2D.SetBool("bossRunning", true);
            }

            // Call a function to make the boss character face towards the player.
            faceTowardsPlayer();

            // Check if the boss's health has reached or fallen below zero.
            if (bossHealth <= 0)
            {
                // Activate the portal game object so that te player can reach it and complete the game. 
                portal.SetActive(true);
                // Instantiate a particle effect at the boss's current position with no rotation.
                Instantiate(particleEffect, transform.position, Quaternion.identity);
                // Destroy the boss game object to remove it from the scene.
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Make the boss character face towards the player.
    /// </summary>
    void faceTowardsPlayer()
    {
        // Get the current scale of the boss character.
        Vector3 faceTowardsPlayerScale = transform.localScale;

        // Check the relative position of the player and boss to determine the direction.
        if (playerPos.position.x >= transform.position.x)
        {
            // If the player is to the right, set the X scale to its absolute value (positive).
            faceTowardsPlayerScale.x = Mathf.Abs(faceTowardsPlayerScale.x);
        }
        else
        {
            // If the player is to the left, set the X scale to its absolute value (positive) and invert it (negative).
            faceTowardsPlayerScale.x = Mathf.Abs(faceTowardsPlayerScale.x) * -1;
        }
        // Update the boss character's local scale to face towards the player.
        transform.localScale = faceTowardsPlayerScale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, .3f);
    }

    /// <summary>
    /// Inflict damage on the boss character.
    /// </summary>
    /// <param name="damage">The amount of damage to deal to the boss.</param>
    public void TakeDamage(int damage)
    {
        // Subtract the specified damage from the boss's health.
        bossHealth -= damage;
    }

    /// <summary>
    /// handle the bosses health bar to represent its health
    /// </summary>
    void HandleHealthBar()
    {
        // make a value called fillValue whcih holds the percentage (in decimals)
        // of how much of the helth of the boss it left
        float fillValue = bossHealth;
        // print this fill value 
        Debug.Log(fillValue);
        // and make the value of the slider equal to this fill value
        slider.value = fillValue;
        // make the slider look at the main camera position so it always faces in the direction of the camera
        slider.transform.LookAt(Camera.main.transform);
        // rotate it 180 degrees so it alwasys faces the camera
        slider.transform.Rotate(0, 180, 0);
    }
}
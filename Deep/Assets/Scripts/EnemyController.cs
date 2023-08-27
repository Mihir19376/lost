using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // The movement speed of the enemies.
    float enemyMoveSpeed = 1f;
    // The transform representing the ground check position the enmy uses to where the edge of the platform is. 
    public Transform groundCheck;
    // The radius for checking ground collision.
    private float checkRadius = .05f;
    // The layer mask for identifying ground objects.
    public LayerMask groundLayer;
    // The maximum health of the enemy.
    int maxEnemyHealth = 5;
    // The current health of the enemy.
    public int currentEnemyHealth;
    // The duration of the dazed state.
    float dazedTime;
    // The initial duration of the dazed state.
    float startDazeTime = .6f;
    // Reference to the player object—you know, to deal damage to it and track it, etc. 
    public GameObject player;
    // The radius for detecting the player for attack.
    float attackRadius = .05f;
    // Reference to the enemy's animator.
    public Animator enemyAnimator2D;
    // The layer mask for identifying the player's layer.
    public LayerMask playerLayer;
    // The time between enemy attacks.
    private float timeBetweenAttack;
    // The initial time between enemy attacks.
    private float startTimeBetweenAttack = .3f;
    // Reference to a particle effect game object—used for the enemies death.
    public GameObject particleEffect;
    // Start is called before the first frame update
    void Start()
    {
        // set the health to max.
        currentEnemyHealth = maxEnemyHealth;
        // find the player game object. 
        player = GameObject.FindGameObjectWithTag("playerTag");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the enemy is not dazed 
        if (dazedTime <= 0)
        {
            // Check if the enemy overlaps with the player within the attack radius and player's layer.
            if (Physics2D.OverlapCircle(transform.position, attackRadius, playerLayer))
            {
                // Set the "EnemyAttacking" parameter in the enemy's animator to true to indicate an attack, and start that animation.
                enemyAnimator2D.SetBool("EnemyAttacking", true);
                // Check if it's time for the enemy to attack based on timeBetweenAttack.
                if (timeBetweenAttack <= 0)
                {
                    // Inflict damage to the player by calling TakePlayerDamage on the player objects script.
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
                // If the enemy is not overlapping with the player, it's not attacking.
                enemyAnimator2D.SetBool("EnemyAttacking", false);
                // Move the enemy horizontally at a constant speed.
                transform.Translate(Time.deltaTime * enemyMoveSpeed * transform.right);

                // Check if the enemy is no longer grounded using the groundCheck position and radius.
                if (!Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer))
                {
                    // Check if the enemy is no longer grounded using the groundCheck position and radius.
                    Flip();
                }
            }
        }

        // Check if the enemy is currently in a dazed stat.
        if (dazedTime > 0)
        {
            // Decrease the dazedTime timer by deltaTime.
            dazedTime -= Time.deltaTime;
        }

        // Check if the enemy's current health has fallen to or below 0.
        if (currentEnemyHealth <= 0)
        {
            // Call a function to handle the enemy's death.
            Die();
        }
    }

    /// <summary>
    /// flip the enemies direction
    /// </summary>
    void Flip()
    {
        // Get the current scale of the enemy.
        Vector3 enemyScale = transform.localScale;
        // Invert the x scale to change the enemy's facing direction.
        enemyScale.x *= -1;
        // Update the enemy's local scale to flip its direction.
        transform.localScale = enemyScale;
        // Invert the enemy's movement speed to match the new direction.
        enemyMoveSpeed *= -1;
    }

    /// <summary>
    /// Inflict damage on the enemy and initiate a dazed state.
    /// </summary>
    /// <param name="damage">The amount of damage to deal to the enemy.</param>
    public void TakeDamage(int damage)
    {
        // Set the dazed time to the initial daze time.
        dazedTime = startDazeTime;
        // Subtract the specified damage from the enemy's current health.
        currentEnemyHealth -= damage;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, .1f);
    }

    /// <summary>
    /// Handle the enemy's death.
    /// </summary>
    void Die()
    {
        // Call a function on the player object to collect a gem—because that's what they earn for killing an enemy. 
        player.GetComponent<PlayerMovement2D>().CollectGem();
        // Instantiate a particle effect at the enemy's position with no rotation.
        Instantiate(particleEffect, transform.position, Quaternion.identity);
        // Destroy the enemy game object to remove it from the scene.
        Destroy(gameObject);
    }
}

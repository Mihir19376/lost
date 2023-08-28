using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBlockController : MonoBehaviour
{
    public LayerMask axeLayer; // Layer mask to detect the player's axe.
    private int mineBlockHealth; // Health of the mineable block.
    private bool takingDamage; // Flag to prevent multiple damage instances.
    public GameObject particleEffect; // Particle effect to be instantiated when the block is destroyed.
    float blockDamageWaitTime = .5f; // Time between the block can be attacked.
    int mineBlockMaxHealth = 3; // self-explanitory.
    int damageAmount = 1; // Amount of damage to be taken.
    // Start is called before the first frame update
    void Start()
    {
        mineBlockHealth = mineBlockMaxHealth; // Initialize the block's health.
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player's axe overlaps with the block, and the block is not currently taking damage.
        // the .05f if hard coded due to the fact it is just mean to be a small distance. 
        if (Physics2D.OverlapCircle(transform.position, .05f, axeLayer) && !takingDamage)
        {
            // Start coroutine to apply damage to the block.
            StartCoroutine(takeBlockDamage(damageAmount));
        }

        // Check if the block's health has reached or fallen below zero.
        if (mineBlockHealth <= 0)
        {
            // Create a simple particle effect in its place.
            Instantiate(particleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destroy itself.
        }
    }

    /// <summary>
    /// Coroutine to apply damage to the block and control the delay between damage.
    /// </summary>
    /// <param name="damageAmount"></param>
    IEnumerator takeBlockDamage(int damageAmount)
    {
        takingDamage = true; // Set the flag to indicate that the block is taking damage.
        mineBlockHealth -= damageAmount; // Reduce the block's health.
        yield return new WaitForSeconds(blockDamageWaitTime); // Wait for a specified duration.
        takingDamage = false; // Reset the damage flag.
    }
}

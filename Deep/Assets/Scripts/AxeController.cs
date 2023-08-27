using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    // the animator for the axe
    public Animator axeAnimator;
    // The layer mask for detecting enemies.
    public LayerMask enemyLayer;
    // The layer mask for detecting the boss.
    public LayerMask bossLayer;
    // The time between axe attacks.
    private float timeBetweenAttack;
    // The initial time between axe attacks.
    private float startTimeBetweenAttack = .3f;
    // Reference to a boss game object.
    GameObject boss;
    // The attack radius.
    float attackRadius = .3f;

    // Start is called before the first frame update
    void Start()
    {
        // find the boss object using its tag in the scene. 
        boss = GameObject.FindGameObjectWithTag("bossTag");
    }

    // Update is called once per frame
    void Update()
    {
        // Start a co-routine that destroys the axe soon.
        StartCoroutine(WaitAndDissolve());
        // Find and reference the player game object by tag "axeSpawnPosTag."
        GameObject player;
        player = GameObject.FindGameObjectWithTag("axeSpawnPosTag");

        // Get the player's position.
        Transform playerPos = player.transform;
        transform.position = playerPos.position;

        // Check if it's time for the axe to attack based on timeBetweenAttack.
        if (timeBetweenAttack <= 0)
        {
            // Find all enemy colliders within a certain radius of the player's position.
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(playerPos.position, attackRadius, enemyLayer);

            // Loop through the list of enemies and inflict damage to each of them.
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage(1);
            }

            // Check if the player's position overlaps with a boss object.
            if (Physics2D.OverlapCircle(playerPos.position, attackRadius, bossLayer))
            {
                // Inflict damage to the boss object.
                boss.GetComponent<BossController>().TakeDamage(1);
            }

            // Reset the timeBetweenAttack to the initial value.
            timeBetweenAttack = startTimeBetweenAttack;
        }
        else
        {
            // Decrease the timeBetweenAttack timer by deltaTime to control the attack rate.
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    IEnumerator WaitAndDissolve()
    {
        yield return new WaitForSeconds(axeAnimator.GetCurrentAnimatorStateInfo(0).length + axeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Debug.Log("Mosjvbhfr");
        Destroy(gameObject);
    }

}

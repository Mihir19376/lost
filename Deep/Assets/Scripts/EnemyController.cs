using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float enemyMoveSpeed = 1f;
    public Transform groundCheck;
    private float checkRadius = .05f;
    public LayerMask groundLayer;

    int maxEnemyHealth = 5;
    public int currentEnemyHealth;

    float dazedTime;
    float startDazeTime = .6f;

    public GameObject player;
    float attackDistance = .2f;

    public Animator enemyAnimator2D;

    public LayerMask playerLayer;

    private float timeBetweenAttack;
    private float startTimeBetweenAttack = .3f;
    // Start is called before the first frame update
    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
        player = GameObject.FindGameObjectWithTag("playerTag");
    }

    // Update is called once per frame
    void Update()
    {
        if (dazedTime <= 0)
        {
            //bool isPlayerInAttackRange = Vector2.Distance(transform.position, player.transform.position) <= attackDistance;
            //bool isPlayerOnSamePlatform = transform.position.y < player.transform.position.y;
            if (Physics2D.OverlapCircle(transform.position, .1f, playerLayer))
            {
                //Debug.Log("Is on Level");
                float step = enemyMoveSpeed * Time.deltaTime;

                enemyAnimator2D.SetBool("EnemyAttacking", true);
                if (timeBetweenAttack <= 0)
                {
                    player.GetComponent<PlayerMovement2D>().TakePlayerDamage(1);
                    timeBetweenAttack = startTimeBetweenAttack;
                }
                else
                {
                    timeBetweenAttack -= Time.deltaTime;
                }
            }
            else
            {
                enemyAnimator2D.SetBool("EnemyAttacking", false);
                transform.Translate(Time.deltaTime * enemyMoveSpeed * transform.right);
                if (!Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer))
                {
                    Flip();
                }
            }
        }

        if (dazedTime > 0)
        {
            dazedTime -= Time.deltaTime;
        }
    }

    void Flip()
    {
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
        enemyMoveSpeed *= -1;
    }

    public void TakeDamage(int damage)
    {
        dazedTime = startDazeTime;
        currentEnemyHealth -= damage;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, .1f);
    }
}
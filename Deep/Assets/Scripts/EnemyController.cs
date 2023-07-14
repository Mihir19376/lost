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
    // Start is called before the first frame update
    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (dazedTime <= 0)
        {
            transform.Translate(Time.deltaTime * enemyMoveSpeed * transform.right);
            if (!Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer))
            {
                Flip();
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
}

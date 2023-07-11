using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float enemyMoveSpeed = 1f;
    public Transform groundCheck;
    private float checkRadius = .1f;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * enemyMoveSpeed * transform.right);
        if (!Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer))
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
        enemyMoveSpeed *= -1;
    }
}

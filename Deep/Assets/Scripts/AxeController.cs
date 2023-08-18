using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public Animator axeAnimator;
    public LayerMask enemyLayer;

    private float timeBetweenAttack;
    private float startTimeBetweenAttack = .3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitAndDissolve());

        GameObject player;
        player = GameObject.FindGameObjectWithTag("axeSpawnPosTag");
        Transform playerPos = player.transform;
        transform.position = playerPos.position;

        if (timeBetweenAttack <= 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(playerPos.position, .3f, enemyLayer);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage(1);
            }
            timeBetweenAttack = startTimeBetweenAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    IEnumerator WaitAndDissolve()
    {
        yield return new WaitForSeconds(axeAnimator.GetCurrentAnimatorStateInfo(0).length + axeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Debug.Log("Mosjvbhfr");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "minableBlockTag")
        {
            Destroy(collision.gameObject);
        }
    }
}

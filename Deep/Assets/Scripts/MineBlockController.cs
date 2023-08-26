using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBlockController : MonoBehaviour
{
    public LayerMask axeLayer;

    private int mineBlockHealth;
    private bool takingDamage;

    public GameObject particleEffect;
    // Start is called before the first frame update
    void Start()
    {
        mineBlockHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, .05f, axeLayer) && !takingDamage)
        {
            StartCoroutine(takeBlockDamage(1));
        }

        if (mineBlockHealth <= 0)
        {
            // destroy
            Instantiate(particleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator takeBlockDamage(int damageAmount)
    {
        takingDamage = true;
        mineBlockHealth -= 1;
        yield return new WaitForSeconds(.5f);
        takingDamage = false;
    }
}

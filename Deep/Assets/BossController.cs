using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool bossLevelInitiated;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        if (bossLevelInitiated)
        {
            // follow player
            if (Physics2D.OverlapCircle(transform.position, .05f, playerLayer))
            {
                // attack player
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
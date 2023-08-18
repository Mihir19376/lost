using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBlockController : MonoBehaviour
{
    public LayerMask axeLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, .05f, axeLayer))
        {
            Destroy(gameObject);
        }
    }
}

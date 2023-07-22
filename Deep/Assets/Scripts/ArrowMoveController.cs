using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMoveController : MonoBehaviour
{
    public Rigidbody2D arrowRb;
    public float directionMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        arrowRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        arrowRb.velocity = new Vector2(5*directionMultiplier, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "mapWallsTag")
        {
            Destroy(gameObject);
        }
    }
}

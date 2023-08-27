using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmeraldController : MonoBehaviour
{
    // this is the amplitude of the sin wave used to bob the gem up and down
    float bobbingAmplitude = .05f;
    // this the frequency of that bob
    float bobbingFrequency = 1f;
    // This is the origin position of the gem.
    Vector3 posOrigin = new Vector3();
    // and this is its temporary position (current pos)
    Vector3 tempPos = new Vector3();
    // The layer mask for detecting the player.
    public LayerMask playerLayer;
    // This is a refrecen to the player chracter itself.
    public GameObject player;
    // Reference to a particle effect game object used to signify the plaeyr collection the gem.
    public GameObject particleEffect;
    // Start is called before the first frame update
    void Start()
    {
        // set the rorigin to be the starting position of the gem
        posOrigin = transform.position;
        // find the player game object through its tag
        player = GameObject.FindGameObjectWithTag("playerTag");
    }

    // Update is called once per frame
    void Update()
    {
        // Store the initial position of the object in tempPos (so that we can use its x and z axis)
        tempPos = posOrigin;
        // Change the y position of the gem based on a sin curve. 
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * bobbingFrequency) * bobbingAmplitude;
        // And change that position. 
        transform.position = tempPos;

        // Check for collisions with objects on the playerLayer using a small circle centered at the object's position.
        if (Physics2D.OverlapCircle(transform.position, .05f, playerLayer))
        {
            // Collect a gem by calling the CollectGem() method on the player's script.
            player.GetComponent<PlayerMovement2D>().CollectGem();
            // Create a particle effect at the object's position.
            Instantiate(particleEffect, transform.position, Quaternion.identity);
            // destroy the gem
            Destroy(gameObject);
        }
    }

}

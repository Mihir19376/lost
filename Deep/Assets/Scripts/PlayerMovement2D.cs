using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement2D : MonoBehaviour
{
    // this is a reference to the Brackeys character controller C# asset
    public CharacterController2D controller2D;
    // this is the run speed of the the character. 
    public float runSpeed = 40f;
    // this is where I store the horizontal movement input of the player.
    private float horizontalMove = 0f;
    // this is a boolean to denote if the character is jumping or not
    private bool jump = false;
    // this is a reference to the characters animator
    public Animator animator2D;
    // this is the axe that spawns with the player to attack/min.
    public GameObject axePrefab;
    // this is a reference to the spawn positon of it (its the transform of an empty game object.)
    public Transform spawnPos;
    // this int (-1 or 1) is multiplied to the spanwed axe's scale to make sure its facing the right way.
    int axeDirectionMultiplier;
    // this denotes whether we are attacking or not
    bool isAttacking = false;
    // this is the curent player's health. 
    public float currentPlayerHealth;
    // this is the beggnign health. 
    public float maxPlayerHealth = 10;
    // this is the number of gems the player has collected.
    public int gems;
    // this is the rising heartbeat in the background sound effect.
    public AudioSource heartBeatAudio;
    // this is the inital distacnce to the boss level based on the playes spawn pos.
    float initialDistanceToBossLevel;
    // this is the object used to calculate that distance.
    public GameObject bossBarrier;
    // this is a reference to the boss. 
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        // Set the volume of the heart beat to 0
        heartBeatAudio.volume = 0;
        // Calculate the initial distance to the boss level by measuring the distance between
        // the position of the 'bossBarrier' and the current object's position
        initialDistanceToBossLevel = Vector3.Distance(bossBarrier.transform.position, transform.position);
        // Set the current player health to its maximum value.
        currentPlayerHealth = maxPlayerHealth;
        // Initialize the 'gems' variable to 0, indicating that the player has collected no gems yet
        gems = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Call a function to manage audio.
        ManageAudio();

        // Get the horizontal input for player movement and multiply it by the runSpeed.
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // Update the animator's "Speed" parameter with the absolute value of horizontalMove,
        // for controlling the characters run anim based on movement speed.
        animator2D.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // Check if the player pressed the "Jump" button.
        if (Input.GetButtonDown("Jump"))
        {
            // Set the 'jump' flag to true, indicating the player wants to jump.
            jump = true;
            // Update the animator to indicate that the character is jumping.
            animator2D.SetBool("IsJumping", true);
        }

        // Check if the player pressed the "Fire1" button and is not already attacking.
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            // Start a coroutine to spawn an axe.
            StartCoroutine(SpawnAxe());
        }

        // Check if the player's health is below or equal to 0
        if (currentPlayerHealth <= 0)
        {
            // Start a coroutine to handle the player's death
            StartCoroutine(Die());
        }
    }

    /// <summary>
    /// when the character lands. this function is called externally. 
    /// </summary>
    public void OnLanding()
    {
        // Update the animator to indicate that the character is no longer jumping.
        animator2D.SetBool("IsJumping", false);
    }

    // This method is called during the FixedUpdate physics update.
    private void FixedUpdate()
    {
        // Use the 'controller2D' to move the character horizontally based on 'horizontalMove'.
        // 'Time.fixedDeltaTime' is used to make the movement frame rate independent.
        // 'jump' is just letting the controller know whether or not the character has jumped.
        // the second parameter is for crouching. I don't use that. 
        controller2D.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        // Reset the 'jump' flag after the character has potentially jumped.
        jump = false;
    }

    /// <summary>
    /// Coroutine to spawn an axe as a weapon.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnAxe()
    {
        // Determine the direction of the axe based on the character's local scale.
        if (transform.localScale.x < 0)
        {
            axeDirectionMultiplier = -1;
        }
        else
        {
            axeDirectionMultiplier = 1;
        }
        // Instantiate the axe object at the spawn position with no rotation.
        GameObject newObject = Instantiate(axePrefab, spawnPos.position, Quaternion.identity) as GameObject;
        // change the x scale to match up with the multiplier. 
        newObject.transform.localScale  = new Vector3(newObject.transform.localScale.x * axeDirectionMultiplier, newObject.transform.localScale.y, newObject.transform.localScale.z); // change its local scale in x y z format
        // Set the 'isAttacking' flag to true to indicate that the character is currently attacking.
        isAttacking = true;
        // Get the animator component of the spawned axe.
        Animator axeAnimator = newObject.GetComponent<Animator>();
        // Wait for a duration equal to the length of the axe's animation.
        yield return new WaitForSeconds(axeAnimator.GetCurrentAnimatorStateInfo(0).length + axeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        // Set the 'isAttacking' flag to false to indicate that the attack animation is complete.
        isAttacking = false;
    }

    /// <summary>
    /// Inflict damage on the player character.
    /// </summary>
    /// <param name="damageToDeal">The amount of damage to deal to the player.</param>
    public void TakePlayerDamage(int damageToDeal)
    {
        // Subtract the specified damage from the player's current health.
        currentPlayerHealth -= damageToDeal;
    }

    /// <summary>
    /// Coroutine to handle the player character's death.
    /// </summary>
    /// <returns></returns>
    IEnumerator Die()
    {
        // Trigger the death animation by setting the "isDead" parameter in the animator to true.
        animator2D.SetBool("isDead", true);
        // Wait for a duration equal to the length of the death animation.
        yield return new WaitForSeconds(animator2D.GetCurrentAnimatorStateInfo(0).length);
        // Load the lose screen.
        SceneManager.LoadScene(2);
        // Output a debugging message indicating that the player has died.
        Debug.Log("Player Died :)");
    }

    /// <summary>
    /// Increment the number of collected gems by one.
    /// </summary>
    public void CollectGem()
    {
        // Increase the count of collected gems by one.
        gems += 1;
    }

    /// <summary>
    /// Manages audio based on the player's distance to the boss level.
    /// </summary>
    void ManageAudio()
    {
        // Check if 'bossBarrier' is not null to avoid null reference errors.
        if (bossBarrier != null)
        {
            // Calculate the distance between the player and the boss barrier.
            float distanceToBossLevel = Vector3.Distance(transform.position, bossBarrier.transform.position);
            // Adjust the volume of the 'heartBeatAudio' based on the distance to the boss level.
            // The volume incrweases as the player gets closer to the boss level.
            heartBeatAudio.volume = 1 - (distanceToBossLevel / initialDistanceToBossLevel);
        }
    }

    /// <summary>
    /// Called when a 2D collision occurs.
    /// </summary>
    /// <param name="collision">The collision data.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "portalTag" tag.
        if (collision.gameObject.tag == "portalTag")
        {
            // Load the win scene. The player has won the game.
            SceneManager.LoadScene(3);
        }
        
    }

    /// <summary>
    /// Called when a 2D trigger collision occurs.
    /// </summary>
    /// <param name="collision">The trigger collision data.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the trigger collided object has the "waterBlockTag" tag.
        if (collision.gameObject.tag == "waterBlockTag")
        {
            // Start the coroutine to handle the player character's death.
            StartCoroutine(Die());
        }
    }
}

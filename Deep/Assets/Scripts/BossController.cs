using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public bool bossLevelInitiated;
    public LayerMask playerLayer;

    private Transform playerPos;
    public float bossSpeed = .2f;

    public Animator bossAnimator2D;

    private float timeBetweenAttack;
    private float startTimeBetweenAttack = .3f;
    public GameObject player;

    public int bossHealth;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("playerTag");
        bossHealth = 10;
    }

    // Update is called once per frame
    void Update()
    {
        HandleHealthBar();
        if (bossLevelInitiated)
        {
            // follow player
            if (Physics2D.OverlapCircle(transform.position, .3f, playerLayer))
            {
                // attack player
                bossAnimator2D.SetBool("bossAttacking", true);
                bossAnimator2D.SetBool("bossRunning", false);
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
                // move towarsa plasy
                playerPos = GameObject.FindGameObjectWithTag("playerTag").GetComponent<Transform>();
                Vector2 target = new Vector2(playerPos.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, target, bossSpeed * Time.deltaTime);
                bossAnimator2D.SetBool("bossAttacking", false);
                bossAnimator2D.SetBool("bossRunning", true);
            }

            faceTowardsPlayer();

            if (bossHealth <= 0)
            {
                SceneManager.LoadScene(3);
            }
        }
    }

    void faceTowardsPlayer()
    {
        Vector3 faceTowardsPlayerScale = transform.localScale;

        if (playerPos.position.x >= transform.position.x)
        {
            faceTowardsPlayerScale.x = Mathf.Abs(faceTowardsPlayerScale.x);
        }
        else
        {
            faceTowardsPlayerScale.x = Mathf.Abs(faceTowardsPlayerScale.x) * -1;
        }

        transform.localScale = faceTowardsPlayerScale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, .3f);
    }

    public void TakeDamage(int damage)
    {
        bossHealth -= damage;
    }

    /// <summary>
    /// handle the bosses health bar to represent its health
    /// </summary>
    void HandleHealthBar()
    {
        // make a value called fillValue whcih holds the percentage (in decimals)
        // of how much of the helth of the boss it left
        float fillValue = bossHealth;
        // print this fill value 
        Debug.Log(fillValue);
        // and make the value of the slider equal to this fill value
        slider.value = fillValue;
        // make the slider look at the main camera position so it always faces in the direction of the camera
        slider.transform.LookAt(Camera.main.transform);
        // rotate it 180 degrees so it alwasys faces the camera
        slider.transform.Rotate(0, 180, 0);
    }
}
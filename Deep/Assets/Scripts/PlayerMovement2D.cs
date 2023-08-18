using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement2D : MonoBehaviour
{
    public CharacterController2D controller2D;
    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private bool jump = false;

    public Animator animator2D;

    public GameObject axePrefab;
    public Transform spawnPos;
    int axeDirectionMultiplier;
    bool isAttacking = false;

    public float currentPlayerHealth;
    public float maxPlayerHealth = 10;


    public int gems;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayerHealth = maxPlayerHealth;
        gems = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator2D.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator2D.SetBool("IsJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isAttacking)
        {
            StartCoroutine(SpawnAxe());
        }

        // if player health is below or equal to 0, die :)
        if(currentPlayerHealth <= 0)
        {
            //die
            Die();
        }
    }

    public void OnLanding()
    {
        Debug.Log("Mon");
        animator2D.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {
        controller2D.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    private IEnumerator SpawnAxe()
    {
        if (transform.localScale.x < 0)
        {
            axeDirectionMultiplier = -1;
        }
        else
        {
            axeDirectionMultiplier = 1;
        }
        GameObject newObject = Instantiate(axePrefab, spawnPos.position, Quaternion.identity) as GameObject;  // instatiate the object
        newObject.transform.localScale  = new Vector3(newObject.transform.localScale.x * axeDirectionMultiplier, newObject.transform.localScale.y, newObject.transform.localScale.z); // change its local scale in x y z format
        isAttacking = true;
        Animator axeAnimator = newObject.GetComponent<Animator>();
        yield return new WaitForSeconds(axeAnimator.GetCurrentAnimatorStateInfo(0).length + axeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        isAttacking = false;
    }

    public void TakePlayerDamage(int damageToDeal)
    {
        currentPlayerHealth -= damageToDeal;
    }

    void Die()
    {
        //die
        SceneManager.LoadScene(2);
        Debug.Log("Player Died :)");

    }

    public void CollectGem()
    {
        gems += 1;
    }
}

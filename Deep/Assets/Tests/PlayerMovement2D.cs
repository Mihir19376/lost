using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public CharacterController2D controller2D;
    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private bool jump = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        controller2D.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}

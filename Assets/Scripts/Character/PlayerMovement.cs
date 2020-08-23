using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator _animator;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool moving = false;

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        moving = horizontalMove != 0;

        jump = Input.GetButtonDown("Jump");

        if (Input.GetButtonDown("Crouch"))
            crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;
    }

    // FixedUpdate is called a fixed amount of time per second
    void FixedUpdate()
    {
        _animator.SetBool("isMoving", moving);
        _animator.SetBool("isJumping", jump);
        _animator.SetBool("isCrouching", jump);
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        
    }
}

using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float jumpHeight;
    public float moveSpeed;
    public float fallMultiplier;
    private bool isJumping = false;
    private bool isMoving = false;
    private bool isGrounded = false;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;

    // Awake is called when object is instantiated
    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        DoMovement();
        DoJump();
    }

    /*
     * function DoMovement
     * 
     * move the player left or right based on input
     * flip the character sprite accordingly
     * trigger walk animation
     */
    private void DoMovement()
    {
        isMoving = false;
        if (Input.GetButton("Horizontal"))
        {
            Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            transform.Translate(moveDirection * Time.deltaTime * moveSpeed);
            isMoving = true;
            _spriteRenderer.flipX = moveDirection.x <= 0;
        }
        _animator.SetBool("isMoving", isMoving);
    }

    /*
     * function DoJump
     * 
     * apply the upward force when the jump button is pressed
     */
    private void DoJump()
    {
        //check if the player is grounded
        if (!isGrounded)
            if (Physics2D.Raycast(transform.position, new Vector2(0, Mathf.Abs(_boxCollider2D.size.y / 2 + 0.37f) * -1)))
            {
                isGrounded = true;
                isJumping = false;
            }

        //do the jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _rigidBody2D.velocity = Vector2.up * jumpHeight;
            isJumping = true;
        }

        //increase gravity after apex of jump
        if (isJumping)
            if (GetShouldApplyPlayerFallSpeed())
                ApplyFallSpeed();

        
        //trigger jump animation
        //TODO: add a jump animation to trigger
        _animator.SetBool("isJumping", isJumping);
    }

    /*
     * function GetShouldApplyPlayerFallSpeed
     * 
     * should apply fall speed if y axis velocity is negative, or if the player stops holding the jump button before the apex
     */
    private bool GetShouldApplyPlayerFallSpeed()
    { 
        return _rigidBody2D.velocity.y < 0 || (_rigidBody2D.velocity.y > 0 && !(Input.GetButton("Jump")));
    }

    /*
     * function ApplyFallSpeed
     * 
     * apply fall multiplier to gravity to make falling more satisfying
     */
    private void ApplyFallSpeed()
    {
        _rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }
}

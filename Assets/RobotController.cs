using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float jumpHeight;
    public float moveSpeed;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    private bool isJumping = false;
    private bool isMoving = false;
    private Vector2 direction = Vector2.right;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    // Awake is called when object is instantiated
    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = false;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _rigidBody2D.MovePosition(_rigidBody2D.position + Vector2.left * Time.deltaTime * moveSpeed);
            //transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
            isMoving = true;
            direction = Vector2.left;
            _spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _rigidBody2D.MovePosition(_rigidBody2D.position + Vector2.right * Time.deltaTime * moveSpeed);
            isMoving = true;
            direction = Vector2.right;
            _spriteRenderer.flipX = false;
        }

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && !isJumping)
        {
            _rigidBody2D.velocity = Vector2.up * jumpHeight;
            isJumping = true;
        }

        if (_rigidBody2D.velocity.y < 0)
        {
            _rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rigidBody2D.velocity.y > 0 && !(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)))
        {
            _rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        _animator.SetBool("isJumping", isJumping);
        _animator.SetBool("isMoving", isMoving);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platforms")
        {
            isJumping = false;
        }
    }
}

using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float jumpHeight;
    public float moveSpeed;
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
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
            isMoving = true;
            direction = Vector2.left;
            _spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
            isMoving = true;
            direction = Vector2.right;
            _spriteRenderer.flipX = false;
        }

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && !isJumping)
        {
            _rigidBody2D.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            isJumping = true;
        }

        _animator.SetBool("isJumping", isJumping);
        _animator.SetBool("isMoving", isMoving);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platforms")
        {
            isJumping = false;
        }
    }
}

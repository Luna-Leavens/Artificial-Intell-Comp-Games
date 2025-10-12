using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed =4f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool doubleJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded()) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            doubleJump = true;
        }else if (Input.GetButtonDown("Jump") && !isGrounded() && doubleJump) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            doubleJump = false;
        }

        Flip();

    }

    private void FixedUpdate() {

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool isGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip() {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal >0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

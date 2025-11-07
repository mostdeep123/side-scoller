using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 7f;
    public float gravityMultiplier = 5f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private bool jumpQueued;
    private bool backRun;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            anim.SetTrigger("jump");
            jumpQueued = true;
            backRun = true;
        }
    }

    void FixedUpdate()
    {
        if (jumpQueued)
        {
            JumpAction();
            jumpQueued = false;
        }
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (gravityMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void JumpAction()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("tile") && !isGrounded)
        {
            isGrounded = true;
            if (backRun)
            {
                backRun = false;
                anim.SetTrigger("run");
            }
        }
    }

    private void OnCollisionStay2D (Collision2D coll)
    {
        if(coll.transform.CompareTag("tile"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("tile"))
        {
            isGrounded = false;
        }
    }
}

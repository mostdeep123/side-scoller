using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 7f;
    public float gravityMultiplier = 5f;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            JumpAction();
        } 
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (gravityMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void JumpAction()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("tile"))
        {
            this.GetComponent<Animator>().SetTrigger("run");
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("tile"))
        {
            this.GetComponent<Animator>().SetTrigger("jump");
            isGrounded = false;
        }
    }
}

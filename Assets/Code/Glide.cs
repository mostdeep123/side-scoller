using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Glide : MonoBehaviour
{
    [Header("Slide Settings")]
    public float slideDuration = 0.8f;
    public float slideSpeedMultiplier = 1.5f;
    public Vector2 slideColliderSize = new Vector2(1f, 0.5f);
    public Vector2 slideColliderOffset = new Vector2(0f, -0.25f);

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D col;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private bool isSliding = false;
    private float slideTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        originalColliderSize = col.size;
        originalColliderOffset = col.offset;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !isSliding)
            StartSlide();

        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0f)
                EndSlide();
        }
    }

    void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;

        col.size = slideColliderSize;
        col.offset = slideColliderOffset;

        if (anim != null)
            anim.SetTrigger("slide");

        rb.linearVelocity = new Vector2(rb.linearVelocity.x * slideSpeedMultiplier, rb.linearVelocity.y);
    }

    void EndSlide()
    {
        isSliding = false;
        col.size = originalColliderSize;
        col.offset = originalColliderOffset;

        if (anim != null)
            anim.SetTrigger("run");
    }
}

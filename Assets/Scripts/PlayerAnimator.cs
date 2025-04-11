using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;

    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animator.SetBool("isWalking", Mathf.Abs(rb.linearVelocity.x) > 0.1f);
        animator.SetBool("isJumping", !isGrounded);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}


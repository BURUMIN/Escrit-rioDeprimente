using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    public bool canMove = true;
    public Animator animator;

    private Vector2 moveDirection;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canMove)
        {
            moveDirection = Vector2.zero;
            ResetAnimations();
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (animator != null)
        {
            animator.SetBool("AndandoCima", moveY > 0);
            animator.SetBool("AndandoBaixo", moveY < 0);
            animator.SetBool("AndandoDireito", moveX > 0);
            animator.SetBool("AndandoEsquerdo", moveX < 0);
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = moveDirection * speed;
    }

    public void SetCanMove(bool value)
    {
        canMove = value;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (!value)
        {
            moveDirection = Vector2.zero;
            ResetAnimations();
        }
    }

    private void ResetAnimations()
    {
        if (animator != null)
        {
            animator.SetBool("AndandoCima", false);
            animator.SetBool("AndandoBaixo", false);
            animator.SetBool("AndandoDireito", false);
            animator.SetBool("AndandoEsquerdo", false);
        }
    }
}
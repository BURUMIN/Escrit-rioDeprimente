using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    public Animator animator;

    private Vector2 moveDirection;
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        animator.SetBool("AndandoCima", moveY>0);
        animator.SetBool("AndandoBaixo", moveY<0); 
        animator.SetBool("AndandoDireito", moveX>0); 
        animator.SetBool("AndandoEsquerdo", moveX<0); 
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

}

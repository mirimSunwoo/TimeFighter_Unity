using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiscesMove : MonoBehaviour
{
    public float movePower = 1f;
    public float jumpPower = 1f;

    Rigidbody2D rigid;
    SpriteRenderer renderer;
    Animator animator;

    Vector3 movement;
    bool isJumping = false;

    //----------------------------------------[Overrid Function]

    // Initialzation
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponentInChildren<SpriteRenderer> ();
        
    }

    // Graphic & Input Updates
    void Update()
    {
        // Moving
        if (Input.GetAxisRaw ("Horizontal") == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetBool("isMoving", true);

            renderer.flipX = false;
        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetBool("isMoving", true);

            renderer.flipX = true;
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            isJumping = true;
            animator.SetBool("isJumping", true);    // Jumping Flog
            animator.SetTrigger("doJumping");   // Jumping Animation
        }
    }

    // Attach Event
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Attach : " + other.gameObject.layer);

        if (other.gameObject.layer == 8 && rigid.velocity.y < 0)
            animator.SetBool("isJumping", false);   // Landing
    }

    // Detach Event
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Detach : " + other.gameObject.layer);
    }

    // Physics engine Updates
    void FixedUpdate()
    {
        Move();
        Jump();
    }

    //-----------------------------------------------------[Movement Function]

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if(Input.GetAxisRaw ("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;

            renderer.flipX = false;
        }

        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;

            renderer.flipX = true;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void Jump()
    {
        if (!isJumping)
            return;

        // Prevent Velocity amplification.
        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
}

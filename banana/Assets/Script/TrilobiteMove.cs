using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrilobiteMove : MonoBehaviour
{
    public float movePower = 5f;
    private float realMovePower = 5f; // 실제 이동 속도
    public float jumpPower = 5f;
    private float realJumpPower = 5f; // 실제 점프 힘
    //public int maxHealth = 3;
    public GameManager gameManager;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    BoxCollider2D boxCollider;

    Vector3 movement;
    bool isJumping = false;

    //----------------------------------------[Overrid Function]

    // Initialzation
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    // Graphic & Input Updates
    void Update()
    {
        // Moving
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetBool("isMoving", true);

            spriteRenderer.flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetBool("isMoving", true);

            spriteRenderer.flipX = false;
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

        //if(OnCollisionEnter2D.gameObject.tag == "portal")
        //{
        //    // Next Stage
        //    gameManager.NextStage();
        //}
        
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

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;

            spriteRenderer.flipX = false;
        }

        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;

            spriteRenderer.flipX = true;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    // 블럭 효과들
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("jumpGround"))
    //    {
    //        realJumpPower = 10 * jumpPower;
    //    }
    //    else if (collision.gameObject.CompareTag("stickGround"))
    //    {
    //        realJumpPower = jumpPower / 2;
    //    }
    //    else
    //    {
    //        realJumpPower = jumpPower;
    //    }
    //    if (collision.gameObject.CompareTag("fastGround"))
    //    {
    //        realMovePower = 2 * movePower;
    //    }
    //    else if (collision.gameObject.CompareTag("slowGround"))
    //    {
    //        realMovePower = movePower / 2;
    //    }
    //    else
    //    {
    //        realMovePower = movePower;
    //    }
    //}

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            onDamaged(collision.transform.position);
        }
    }

    void onDamaged(Vector2 targetPos)
    {
        // Health Down
        gameManager.HealthDown();

        // Change Layer (Immortal Active)
        gameObject.layer = 11;

        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 70, ForceMode2D.Impulse);

        // Animation
        animator.SetTrigger("doDamaged");

        Invoke("OffDamaged", 3);    // 함수 호출
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        // Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        // Sprite Flip Y
        spriteRenderer.flipY = true;
        // Collider Disable
        boxCollider.enabled = false;
        // Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // 죽는 애니메이션 넣기
        // animator.SetTrigger("doDamaged");
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

}

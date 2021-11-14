using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
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
    PolygonCollider2D polygonCollider;

    Vector3 movement;
    bool isJumping = false;

    private float item_jump_cooltime;

    public int RandomInt;

    //----------------------------------------[Overrid Function]

    // Initialzation
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();

    }

    // Graphic & Input Updates
    void Update()
    {
        
        RandomInt = UnityEngine.Random.Range(1, 4);

        // Moving
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetBool("isMoving", true);

            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetBool("isMoving", true);

            spriteRenderer.flipX = true;
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            isJumping = true;
            animator.SetBool("isJumping", true);    // Jumping Flog
            animator.SetTrigger("doJumping");   // Jumping Animation
        }

        if (jumpPower == 20.0f)
        {
            item_jump_cooltime += Time.deltaTime;
            if (item_jump_cooltime > 10)
            {
                jumpPower = 14.0f;
            }
            animator.SetTrigger("doItem");
        }
    }

    // Attach Event
    void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log("Attach : " + collision.gameObject.layer);

        if (collision.gameObject.layer == 8 && rigid.velocity.y < 0)
            animator.SetBool("isJumping", false);   // Landing

        // Enemy hit
        if (collision.gameObject.tag == "Enemy" && !collision.isTrigger && rigid.velocity.y < -6f)  // -6f : 값이 작아질 수록 판정이 약해짐
        {

            //gameObject.layer = 13;
            //spriteRenderer.color = new Color(1, 1, 1, 1);
            

            EnemyMove enemy = collision.gameObject.GetComponent<EnemyMove>();
            enemy.Attack();

            // 뭘까
            //Vector2 killVelocity = new Vector2(0, 13f);
            //rigid.AddForce(killVelocity, ForceMode2D.Impulse);

            //gameManager.HealthUp();


        }

        if (collision.gameObject.tag == "coin")
        {
            // Point
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");

            if (isBronze)
            {
                gameManager.stagePoint += 50;
            }
            else if (isSilver)
            {
                gameManager.stagePoint += 100;
            }
            else if (isGold)
            {
                gameManager.stagePoint += 300;
            }

            // Deactive Item
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "portal")
        {
            // Next Stage
            gameManager.NextStage();
        }
        if (collision.gameObject.tag == "healthItem")
        {
            gameManager.HealthUp();
        }
        if (collision.gameObject.tag == "invincibleItem")
        {
            animator.SetTrigger("doItem");
            StartCoroutine("GetInvincible");
        }
        if (collision.gameObject.tag == "random")
        {
            Random();
        }

    }

    void Random()
    {
        if (RandomInt == 1) // RandomInt가 1이라면
        {
            gameManager.HealthUp();
            Debug.Log("효과 : 생명증가");
        }
        else if (RandomInt == 2)
        {
            StartCoroutine("GetInvincible");
            Debug.Log("효과 : 무적");
        }
        else if (RandomInt == 3)
        {
            jumpPower = 20.0f;
            if (jumpPower == 20.0f)
            {
                item_jump_cooltime += Time.deltaTime;
                if (item_jump_cooltime > 10)
                {
                    jumpPower = 14.0f;
                }
                animator.SetTrigger("doItem");
            }
            Debug.Log("효과 : 점프 증진");
        }
    }

    IEnumerator GetInvincible()//무적아이템 무적
    {
        Physics2D.IgnoreLayerCollision(13, 14, true);
        yield return new WaitForSeconds(10f);
        Physics2D.IgnoreLayerCollision(13, 14, false);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            onDamaged(collision.transform.position);
        }
        if(collision.gameObject.tag == "spikes")
        {
            onDamaged(collision.transform.position);
        }
        if (collision.gameObject.CompareTag("item_jump"))
        {
            jumpPower = 20.0f;
        }
    }

    void onDamaged(Vector2 targetPos)
    {
        // Health Down
        gameManager.HealthDown();

        // Change Layer (Immortal Active)
        gameObject.layer = 12;

        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        // Animation
        //animator.SetTrigger("doDamaged");

        Invoke("OffDamaged", 2);    // 함수 호출
    }

    //void onDamaged(Vector2 targetPos)
    //{
    //    // Health Down
    //    //gameManager.HealthDown();

    //    // Change Layer (Immortal Active)
    //    gameObject.layer = 12;

    //    // View Alpha
    //    spriteRenderer.color = new Color(1, 1, 1, 0.4f);

    //    // Reaction Force
    //    int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
    //    rigid.AddForce(new Vector2(dirc, 1) * 3, ForceMode2D.Impulse);

    //    // Animation
    //    animator.SetTrigger("doDamaged");

    //    Invoke("OffDamaged", 3);    // 함수 호출
    //}

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        // Attack
    //        if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
    //        {
    //            OnAttack(collision.transform);
    //            //PlaySound("ATTACK");
    //            audioSource.clip = audioAttack;
    //            audioSource.Play();
    //        }
    //        else // Damaged
    //            OnDamaged(collision.transform.position);
    //    }
    //}

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag == "Enemy")
    //    {
    //        if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
    //            OnAttack(collision.transform);
    //        onDamaged(collision.transform.position);
    //    }

    //    if (collision.gameObject.CompareTag("item_jump"))
    //    {
    //        jumpPower = 20.0f;
    //    }
    //}

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        // Attack
    //        if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
    //        {
    //            OnAttack(collision.transform);
    //        }
    //        else // Damaged
    //            OnDamaged(collision.transform.position);
    //    }
    //}

    //void OnAttack(Transform enemy)
    //{
    //    // Point
    //    gameManager.stagePoint += 100;

    //    // Reaction Force
    //    rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

    //    // Enemy Die
    //    EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
    //    enemyMove.OnDamaged();
    //}



    void OffDamaged()
    {
        gameObject.layer = 13;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        // Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        // Sprite Flip Y
        //spriteRenderer.flipY = true;
        // Collider Disable
        polygonCollider.enabled = false;
        // Die Effect Jump
        //rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // 죽는 애니메이션 넣기
        animator.SetTrigger("doDie");
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

}
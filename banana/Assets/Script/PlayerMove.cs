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
    }

    // Attach Event
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Attach : " + collision.gameObject.layer);

        if (collision.gameObject.layer == 8 && rigid.velocity.y < 0)
            animator.SetBool("isJumping", false);   // Landing

        if (collision.gameObject.tag == "Item")
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
        }else if(collision.gameObject.tag == "portal")
        {
            // Next Stage
            gameManager.NextStage();
        }

    }



    // Detach Event
    //void OnTriggerExit2D(Collider2D other)
    //{
    //    Debug.Log("Detach : " + other.gameObject.layer);
    //}

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

    //void OnAttack(Transform enemy)
    //{

    //    //Reaction Force : 반동(플레이어가 튕겨져나감)
    //    rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

    //    //Enemy Die
    //    //몬스터에 적용한 스크립트의 함수를 사용하기위해 해당 클래스의 변수를 선언해서 초기화
    //    EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
    //    enemyMove.OnDamaged(); // 몬스터가 데미지를 입었을때 실행할 함수를 불러옴 

    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        //일정속도로 몬스터를 밟게되면
        if (other.gameObject.tag == "Enemy" && !other.isTrigger && rb.velocity.y < -5f)
        {
            Monster creature = other.gameObject.GetComponent<Monster>();
            //몬스터의 Die함수 호출 
            creature.Die();

            //바운스
            Vector2 killVelocity = new Vector2(0, 30f);
            rb.AddForce(killVelocity, ForceMode2D.Impulse);

            //스코어 매니저에 몬스터의 점수 저장
            ScoreManager.setScore(creature.score);
        }

        if (other.gameObject.tag == "Coin")
        {
            GetCoin coin = other.gameObject.GetComponent<GetCoin>();
            ScoreManager.setScore((int)coin.value);

            Destroy(other.gameObject, 0f);
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
        rigid.AddForce(new Vector2(dirc, 1) * 3, ForceMode2D.Impulse);

        // Animation
        animator.SetTrigger("doDamaged");

        Invoke("OffDamaged", 3);    // 함수 호출
    }

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
        spriteRenderer.flipY = true;
        // Collider Disable
        boxCollider.enabled = false;
        // Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // 죽는 애니메이션 넣기
        //animator.SetTrigger("doDie");
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

}

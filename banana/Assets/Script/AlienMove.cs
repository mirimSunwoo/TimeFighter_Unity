using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlienMove : MonoBehaviour
{
    public float moveSpeed = 5.0f; //플레이어 이동 속도
    private float realMoveSpeed = 5.0f; // 실제 이동 속도
    public float jumpPower = 5.0f; //플레이어 점프 힘
    private float realJumpPower = 5.0f; // 실제 점프 힘

    public Rigidbody2D rigid;

    float horizontal; //왼쪽, 오른쪽 방향값을 받는 변수
    public bool isground;   // 값을 받을 bool값
    //public Transform groundCheck;   // player발위치
    //public float groundRadius = 0.2f;   // 측정할 범위
    //public LayerMask whatIsGround; // 어떤 layer를 측정할지
    public Animator animator; // 애니메이터 추가

    //Renderer rend;//스프라이트 렌더러   
    //Color c;//색깔 조정
    //public int health;//현재 생명력
    //public Image[] hearts;//생명력 이미지
    //public Sprite healthSprite;//생명력 스프라이트 

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //rend = GetComponent<Renderer>();
        //c = rend.material.color;
        //health = 3;
    }

    private void FixedUpdate()
    {
        //isground = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        // if(isground == true){
        //      animator.SetBool("jump", false);
        // }

        Move(); //플레이어 이동
        Jump(); //점프   
        //healthCheck();//체력확인
    }

    // 지우지 마..!
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isground = true;//ground에 접촉하면 isground를 true로
        }
        if (collision.gameObject.tag == "Enemy")
        {
            onDamaged(collision.transform.position);
        }

        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    StartCoroutine("GetInvulnerable");
        //}
    }

    void onDamaged(Vector2 targetPos)
    {
        // Change Layer (Immortal Active)
        gameObject.layer = 11;

        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        Invoke("OffDamaged", 3);    // 함수 호출
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


    //IEnumerator GetInvulnerable()
    //{
    //    health--;
    //    Physics2D.IgnoreLayerCollision(13, 14, true);
    //    c.a = 0.5f;
    //    rend.material.color = c;
    //    yield return new WaitForSeconds(3f);
    //    Physics2D.IgnoreLayerCollision(13, 14, false);
    //    c.a = 1f;
    //    rend.material.color = c;
    //}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "portal")
        {
            SceneManager.LoadScene("AlienMove");
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("jumpGround"))
    //    {
    //        realJumpPower = 2 * jumpPower;
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
    //        realMoveSpeed = 2 * moveSpeed;
    //    }
    //    else if (collision.gameObject.CompareTag("slowGround"))
    //    {
    //        realMoveSpeed = moveSpeed / 2;
    //    }
    //    else
    //    {
    //        realMoveSpeed = moveSpeed;
    //    }
    //}


    void Jump()
    {
        if (Input.GetButton("Jump")) //점프 키가 눌렸을 때//ground이면서 스페이스바 누르면 
        {
            if (isground == true) //점프 중이지 않을 때
            {
                animator.SetBool("jump", true);  // 점프하면 애니메이터 설정 jump true
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //위쪽으로 힘을 준다.
                isground = false;
            }
            else return; //점프 중일 때는 실행하지 않고 바로 return.
        }
    }

    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            animator.SetBool("walk", true);  // 애니메이터 설정 walk를 true
            if (horizontal >= 0) this.transform.eulerAngles = new Vector3(0, 0, 0);

            else this.transform.eulerAngles = new Vector3(0, 180, 0);

        }
        else
        {
            animator.SetBool("walk", false);    // 좌우로 안움직이면 애니메이터 설정 walk false.
        }

        Vector3 dir = math.abs(horizontal) * Vector3.right; //변수의 자료형을 맞추기 위해 생성한 새로운 Vector3 변수

        this.transform.Translate(dir * moveSpeed * Time.deltaTime); //오브젝트 이동 함수
    }

    //void healthCheck()
    //{
    //    for (int i = 0; i < hearts.Length; i++)
    //    {
    //        hearts[i].sprite = healthSprite;
    //        if (i < health)
    //        {
    //            hearts[i].enabled = true;
    //        }
    //        else
    //        {
    //            hearts[i].enabled = false;
    //        }
    //        if (health <= 0)
    //        {
    //            SceneManager.LoadScene("Main");//체력 0이면 scene다시불러오기 
    //        }
    //    }
    //}

}
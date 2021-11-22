using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    public float speed;
    bool isLeft = true;


    Vector3 movement;
    int movementFlag = 0;
    bool isTracing = false;
    bool isDie = false;

    public int enemyhealth;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    public Slider HealthBar;

    public GameManager gameManager;

    PolygonCollider2D polygonCollider;
    BoxCollider2D boxCollider;
    CapsuleCollider2D capsuleCollider2D;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

    }

    void ReturnSprite()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "endpoint")
        {
            if (isLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isLeft = true;
            }
        }
    }

    public void Attack()
    {
        enemyhealth--;
        // SlidBar에서 Max Value 조절하기
        HealthBar.value -= 1f;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("ReturnSprite", 1.0f);

        if (enemyhealth == 0)
        {
            // Die 혹은 Destroy
            Die();
            //Destroy(gameObject);
        }
    }

    public void Die()
    {

        ScoreManager score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        score.stagePoint += 300;  // 괴물 죽일 떄 점수

        //코루틴 정지
        StopCoroutine("ChangeMovement");
        isDie = true;

        //Y축반전
        SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        renderer.flipY = true;

        //낙하
        // 다 넣기..
        boxCollider.enabled = false;
        polygonCollider.enabled = false;
        capsuleCollider2D.enabled = false;

        //바운스
        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
        Vector2 dieVelocity = new Vector2(0, 5f);
        rigid.AddForce(dieVelocity, ForceMode2D.Impulse);


        // 적 죽으면 포탈 생김
        gameManager.Portal.SetActive(true);

    }

}
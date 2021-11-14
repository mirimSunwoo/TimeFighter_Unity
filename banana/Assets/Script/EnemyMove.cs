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

    public GameObject GoldCoin;
    public GameObject SilverCoin;
    public GameObject BronzeCoin;
    public GameObject portal;

    public GameManager gameManager;

    PolygonCollider2D polygonCollider;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    //void OnHit(int dmg)
    //{
    //    health -= dmg;
    //    spriteRenderer.color = new Color(1, 1, 1, 0.4f);
    //    Invoke("ReturnSprite", 1.0f);

    //    if (health <= 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

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
        //if (collision.gameObject.tag == "hit")
        //{
        //    Hit hit = collision.gameObject.GetComponent<Hit>();
        //    OnHit(hit.dmg);
        //}
    }

    //public void OnDamaged()
    //{ //몬스터가 데미지를 입었을때 


    //    //Sprite Alpha : 색상 변경 
    //    spriteRenderer.color = new Color(1, 1, 1, 0.4f);

    //    //Sprite Flip Y : 뒤집어지기 
    //    spriteRenderer.flipY = true;

    //    //Collider Disable : 콜라이더 끄기 
    //    polygonCollider.enabled = false;

    //    //Die Effect Jump : 아래로 추락(콜라이더 꺼서 바닥밑으로 추락함 )
    //    rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

    //    //Destroy 
    //    Invoke("DeActive", 5);

    //}

    public void Attack()
    {
        enemyhealth--;
        // SlidBar에서 Max Value 조절하기
        HealthBar.value -= 1f;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("ReturnSprite", 1.0f);

        if (enemyhealth == 0)
        {

            // 괴물 죽었을 때 아이템 드랍

            //int ran = UnityEngine.Random.Range(1, 5);
            //if (ran == 1) // 50%
            //{
            //    Instantiate(SilverCoin, transform.position, SilverCoin.transform.rotation);
            //}
            //else if (ran == 2)   // 80%
            //{
            //    Instantiate(GoldCoin, transform.position, GoldCoin.transform.rotation);
            //}
            //else if (ran == 3)   // 90%
            //{
            //    Instantiate(SilverCoin, transform.position, SilverCoin.transform.rotation);
            //}
            //else if (ran == 4)   // 90%
            //{
            //    Instantiate(BronzeCoin, transform.position, BronzeCoin.transform.rotation);
            //}


            // Die 혹은 Destroy
            Die();
            //Destroy(gameObject);
        }
    }

    public void Die()
    {
        gameManager.stagePoint += 500;  // 괴물 죽일 떄 점수

        //코루틴 정지
        StopCoroutine("ChangeMovement");
        isDie = true;

        //Y축반전
        SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        renderer.flipY = true;

        //낙하
        PolygonCollider2D polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
        polygonCollider.enabled = false;

        //바운스
        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
        Vector2 dieVelocity = new Vector2(0, 5f);
        rigid.AddForce(dieVelocity, ForceMode2D.Impulse);

        //오브젝트 삭제
        Destroy(gameObject, 3.0f);

    }

    //public void OnDie()
    //{
    //    // Sprite Alpha
    //    spriteRenderer.color = new Color(1, 1, 1, 0.4f);
    //    // Sprite Flip Y
    //    spriteRenderer.flipY = true;
    //    // Collider Disable
    //    polygonCollider.enabled = false;
    //    // Die Effect Jump
    //    rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    //    Destroy(gameObject, 5f);

    //}

    //    void DeActive()
    //{
    //    gameObject.SetActive(false);
    //}


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed;
    bool isLeft = true;

    BoxCollider2D boxCollider;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
    //public void OnDamaged()
    //{ //몬스터가 데미지를 입었을때 


    //    //Sprite Alpha : 색상 변경 
    //    spriteRenderer.color = new Color(1, 1, 1, 0.4f);

    //    //Sprite Flip Y : 뒤집어지기 
    //    spriteRenderer.flipY = true;

    //    //Collider Disable : 콜라이더 끄기 
    //    boxCollider.enabled = false;

    //    //Die Effect Jump : 아래로 추락(콜라이더 꺼서 바닥밑으로 추락함 )
    //    rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

    //    //Destroy 
    //    Invoke("DeActive", 5);

    //}

    //public void OnDamaged()
    //{
    //    // Sprite Alpha
    //    spriteRenderer.color = new Color(1, 1, 1, 0.4f);
    //    // Sprite Flip Y
    //    spriteRenderer.flipY = true;
    //    // Collider Disable
    //    polygoncollider.enabled = false;
    //    // Die Effect Jump
    //    rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    //    // Destroy
    //    Invoke("DeActive", 5);
    //}

    void DeActive()
    {
        gameObject.SetActive(false);
    }


}
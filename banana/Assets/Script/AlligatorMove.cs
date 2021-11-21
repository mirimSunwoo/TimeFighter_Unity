using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlligatorMove : MonoBehaviour
{
    public float speed;
    bool isLeft = true;

    //PolygonCollider2D polygoncollider;
    //Rigidbody2D rigid;
    //SpriteRenderer spriteRenderer;

    void Start()
    {
        //rigid = GetComponent<Rigidbody2D>();
        //polygoncollider = GetComponent<PolygonCollider2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
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

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AlligatorMove : MonoBehaviour
//{
//    public float movePower = 1f;

//    Animator animator;
//    Vector3 movement;
//    int movementFlag = 0;

//    void Start()
//    {
//        animator = gameObject.GetComponentInChildren<Animator>();

//        StartCoroutine("ChangeMovement");
//    }

//    IEnumerator ChangeMovement()
//    {
//        Debug.Log("Front Logic : " + Time.time);
//        yield return new WaitForSeconds(5f);
//        Debug.Log("Behind Logic : " + Time.time);
//    }

//    void FixedUpdate()
//    {
//        Move();
//    }

//    void Move()
//    {
//        Vector3 moveVelocity = Vector3.zero;

//        if(movementFlag == 1)
//        {
//            moveVelocity = Vector3.left;
//            transform.localScale = new Vecctor3(1, 1, 1);
//        }else if(movementFlag == 2)
//        {
//            moveVelocity = Vector3.right;
//            transform.localScale = new Vector3(-1, 1, 1);
//        }
//        transform.position += moveVelocity * movePower * Time.deltaTime;
//    }
//}
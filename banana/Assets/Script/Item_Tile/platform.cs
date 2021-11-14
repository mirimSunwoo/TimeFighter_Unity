using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    public Animator animator;//애니메이터 추가
    public float count = 2.0f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, count * Time.deltaTime);
            animator.SetBool("isPlayer", true);//바스라지는 애니메이션 설정.
        }
    }
}
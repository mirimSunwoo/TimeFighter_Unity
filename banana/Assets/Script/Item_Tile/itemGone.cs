using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemGone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)//아이템 먹으면 사라지게
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
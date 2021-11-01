using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public TrilobiteMove player;

    public Image[] UIhealth;
    //public Text UIPoint;
    //public Text UIStage;

    public void NextStage()
    {
        stageIndex++;
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            // Player Die Effect
            player.OnDie();

            // Result UI
            Debug.Log("죽었습니다!");

            // Retry Button UI
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Player Reposition
            if (health > 1)
            {
                collision.attachedRigidbody.velocity = Vector2.zero;
                collision.transform.position = new Vector3(-962, 731, 0);   // Player의 시작지점 x축, y축, z축
            }
            // Health Down
            HealthDown();

        }
    }
}

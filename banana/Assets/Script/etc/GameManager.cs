using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public int totalPoint;
    //public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;

    public GameObject UIImg;
    public GameObject UIRestartBtn;
    public GameObject UIRetryBtn;
    public GameObject UINextBtn;
    public GameObject UIHomeBtn;
    public GameObject ClearStory;
    public GameObject DieStory;
    public GameObject Defeat;
    public GameObject Clear;

    public GameObject Player;
    public GameObject Portal;

    public void NextStage()
    {
        ScoreManager score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        score.stagePoint = 0;
    }

    public void HealthDown()
    {
        if (health == 2)
        {
            health--;
            UIhealth[health].color = new Color(0, 0, 0, 0);
            UIhealth[6].color = new Color(1, 1, 1, 1);
        }
        else if(health == 3)
        {
            health--;
            UIhealth[health].color = new Color(0, 0, 0, 0);
            UIhealth[7].color = new Color(1, 1, 1, 1);
        }
        else if(health > 3)
        {
            health--;
            UIhealth[health].color = new Color(0, 0, 0, 0);
        }
        else
        {
            // All Health UI Off
            UIhealth[0].color = new Color(0, 0, 0, 0);
            UIhealth[5].color = new Color(1, 1, 1, 1);

            // Player Die Effect
            player.OnDie();

            // Result UI
            Debug.Log("죽었습니다!");

            // Retry Button UI
            Invoke("Retry", 2);

        }
    }

    // 죽으면 화면 켜짐
    void Retry()
    {
        Defeat.SetActive(true);
        DieStory.SetActive(true);
        UIImg.SetActive(true);
        UIRetryBtn.SetActive(true);
        UIHomeBtn.SetActive(true);
    }

    public void healthdown()
    {
        if(health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
    }

    public void HealthUp()
    {

        if (health == 1)
        {
            health++;
            UIhealth[1].color = new Color(1, 1, 1, 1);
            UIhealth[6].color = new Color(0, 0, 0, 0);
        }
        else if (health == 2)
        {
            health++;
            UIhealth[2].color = new Color(1, 1, 1, 1);
            UIhealth[7].color = new Color(0, 0, 0, 0);
        }
        else if (health == 3)
        {
            health++;
            UIhealth[3].color = new Color(1, 1, 1, 1);
        }
        else if (health == 4)
        {
            health++;
            UIhealth[4].color = new Color(1, 1, 1, 1);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Player Reposition
            if (health > 1)
            {
                PlayerReposition();
            }
            // Health Down
            HealthDown();

        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(1, 1, -1);      // Player의 시작지점 x축, y축, z축
    }

}

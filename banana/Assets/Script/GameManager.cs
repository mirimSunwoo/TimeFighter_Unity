using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn;

    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    public void NextStage()
    {
        // Change Stage
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + (stageIndex + 1);
        }
        else
        // Game Clear
        {
            // Player Control Lock
            Time.timeScale = 0;
            // Result UI
            Debug.Log("게임 클리어!");
            // Restart Button UI
            UIRestartBtn.SetActive(true);
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Game Clear";
            UIRestartBtn.SetActive(true);
        }

        // Calculate Point
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

                // All Health UI Off
                UIhealth[0].color = new Color(1, 0, 0, 0.4f);

                // Player Die Effect
                player.OnDie();

                // Result UI
                Debug.Log("죽었습니다!");

                // Retry Button UI
                UIRestartBtn.SetActive(true);
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
            player.transform.position = new Vector3(0, 0, 0);      // Player의 시작지점 x축, y축, z축
            player.VelocityZero();
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
    }
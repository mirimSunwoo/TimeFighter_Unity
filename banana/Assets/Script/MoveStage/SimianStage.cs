using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimianStage : MonoBehaviour
{

    public GameObject UIOptionImg;
    public GameManager gameManager;
    // 고치기
    private int SimianPoint = 0;

    public void Awake()
    {
        ScoreManager score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        SimianPoint = score.stagePoint;
    }

    // 화면 내 버튼 기능

    public void Retry()
    {
        ScoreManager score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        score.stagePoint = 0;

        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Next()
    {
        // 고치기
        Time.timeScale = 1;
        SceneManager.LoadScene(10);
    }

    // 옵션 화면
    public void option()
    {
        Time.timeScale = 0;
        UIOptionImg.SetActive(true);
    }
    public void close()
    {
        Time.timeScale = 1;
        UIOptionImg.SetActive(false);
    }

    // 옵션 버튼 기능
    public void restart()
    {
        ScoreManager score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        score.stagePoint = SimianPoint;


        // 고치기
        Time.timeScale = 1;
        SceneManager.LoadScene(9);
    }

}

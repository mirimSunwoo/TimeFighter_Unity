using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explan : MonoBehaviour
{
    //float time;

    //void Update()
    //{
    //    if (time < 0.5f)
    //    {
    //        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - time);
    //    }
    //    else
    //    {
    //        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time);
    //        if (time > 1f)
    //        {
    //            time = 0;
    //        }
    //    }

    //    time += Time.deltaTime;

    //}


    public void RankingClick()
    {
        ResetScore();
        SceneManager.LoadScene(1);
    }

    public void StartClick()
    {
        ResetScore();
        SceneManager.LoadScene(2);
    }

    public void ResetScore()
    {
        ScoreManager score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        if (score.stagePoint != 0)
        {
            score.stagePoint = 0;
        }
    }

    public void Special()
    {
        ResetScore();
        SceneManager.LoadScene(12);
    }
}

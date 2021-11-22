using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstView : MonoBehaviour
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
    public GameObject UIExplainBoard;
    public GameObject UIFirstImg;
    public GameObject UISecondImg;
    public GameObject UIThirdImg;
    public GameObject UIFourthImg;
    public GameObject UIFifthImg;
    public GameObject UISixthImg;
    public GameObject UISeventhImg;

    public GameObject UIFirst_Btn;
    public GameObject UISecondBtn;
    public GameObject UISecond_Btn;
    public GameObject UIThirdBtn;
    public GameObject UIThird_Btn;
    public GameObject UIFourthBtn;
    public GameObject UIFourth_Btn;
    public GameObject UIFifthBtn;
    public GameObject UIFifth_Btn;
    public GameObject UISixthBtn;
    public GameObject UISixth_Btn;
    public GameObject UISeventhBtn;

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

    public void Explain()
    {
        UIExplainBoard.SetActive(true);
    }

    public void SecondExplain()
    {
        UIFirstImg.SetActive(false);
        UIFirst_Btn.SetActive(false);
        UISecondImg.SetActive(true);
        UISecondBtn.SetActive(true);
        UISecond_Btn.SetActive(true);
    }

    public void SecondBackExplain()
    {
        UISecondImg.SetActive(false);
        UISecondBtn.SetActive(false);
        UISecond_Btn.SetActive(false);
        UIFirstImg.SetActive(true);
        UIFirst_Btn.SetActive(true);
    }

    public void ThirdExplain()
    {
        UISecondImg.SetActive(false);
        UISecondBtn.SetActive(false);
        UISecond_Btn.SetActive(false);
        UIThirdImg.SetActive(true);
        UIThirdBtn.SetActive(true);
        UIThird_Btn.SetActive(true);
    }

    public void ThirdBackExplain()
    {
        UIThirdImg.SetActive(false);
        UIThirdBtn.SetActive(false);
        UIThird_Btn.SetActive(false);
        UISecondImg.SetActive(true);
        UISecondBtn.SetActive(true);
        UISecond_Btn.SetActive(true);
    }

    public void fourthExplain()
    {
        UIThirdImg.SetActive(false);
        UIThirdBtn.SetActive(false);
        UIThird_Btn.SetActive(false);
        UIFourthImg.SetActive(true);
        UIFourthBtn.SetActive(true);
        UIFourth_Btn.SetActive(true);
    }

    public void FourthBackExplain()
    {
        UIFourthImg.SetActive(false);
        UIFourthBtn.SetActive(false);
        UIFourth_Btn.SetActive(false);
        UIThirdImg.SetActive(true);
        UIThirdBtn.SetActive(true);
        UIThird_Btn.SetActive(true);
    }

    public void fifthExplain()
    {
        UIFourthImg.SetActive(false);
        UIFourthBtn.SetActive(false);
        UIFourth_Btn.SetActive(false);
        UIFifthImg.SetActive(true);
        UIFifthBtn.SetActive(true);
        UIFifth_Btn.SetActive(true);
    }

    public void FifthBackExplain()
    {
        UIFifthImg.SetActive(false);
        UIFifthBtn.SetActive(false);
        UIFifth_Btn.SetActive(false);
        UIFourthImg.SetActive(true);
        UIFourthBtn.SetActive(true);
        UIFourth_Btn.SetActive(true);
    }

    public void sixthExplain()
    {
        UIFifthImg.SetActive(false);
        UIFifthBtn.SetActive(false);
        UIFifth_Btn.SetActive(false);
        UISixthImg.SetActive(true);
        UISixthBtn.SetActive(true);
        UISixth_Btn.SetActive(true);
    }

    public void SixthBackExplain()
    {
        UISixthImg.SetActive(false);
        UISixthBtn.SetActive(false);
        UISixth_Btn.SetActive(false);
        UIFifthImg.SetActive(true);
        UIFifthBtn.SetActive(true);
        UIFifth_Btn.SetActive(true);
    }

    public void seventhExplain()
    {
        UISixthImg.SetActive(false);
        UISixthBtn.SetActive(false);
        UISixth_Btn.SetActive(false);
        UISeventhImg.SetActive(true);
        UISeventhBtn.SetActive(true);
    }

    public void SevenBackExplain()
    {
        UISeventhImg.SetActive(false);
        UISeventhBtn.SetActive(false);
        UISixthImg.SetActive(true);
        UISixthBtn.SetActive(true);
        UISixth_Btn.SetActive(true);
    }

    public void close()
    {
        Time.timeScale = 1;
        UIExplainBoard.SetActive(false);
    }
}

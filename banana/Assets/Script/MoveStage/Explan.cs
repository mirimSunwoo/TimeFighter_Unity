using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explan : MonoBehaviour
{
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

    public void FirstBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void FirstImg()
    {
        SceneManager.LoadScene(2);
    }

    public void First_Btn()
    {
        SceneManager.LoadScene(1);
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
}

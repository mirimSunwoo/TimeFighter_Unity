using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    public int stagePoint;

    private void Awake()
    {
        var objs = FindObjectsOfType<ScoreManager>();
        if(objs.Length == 1)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        //UIPoint.text = (totalPoint + stagePoint).ToString();
        GUILayout.Label("Score : " + stagePoint.ToString());

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
}

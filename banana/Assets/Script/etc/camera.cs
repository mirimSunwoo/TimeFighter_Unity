using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class camera : MonoBehaviour
{
    public Transform player;//따라다닐 player지정  
    void Update()
    {
        gameObject.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);//카메라가 player를 따라다니게 한다.  
        //카메라의 z위치는 비추려는 대상보다 뒤에있어야 하니까 자체적으로 z값을 준다.
    }
}

// 카메라 최대값, 최솟값
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//public class camera : MonoBehaviour
//{
//    public Transform player;
//    public float xMin;
//    public float yMin;
//    public float xMax;
//    public float yMax;
//    void Update()
//    {
//        float x = Mathf.Clamp(player.position.x, xMin, xMax);//어떤 값의 최대값과 최솟값을 정해주는 함수.
//        float y = Mathf.Clamp(player.position.y, yMin, yMax);
//        gameObject.transform.position = new Vector3(x, y, this.transform.position.z);
//        //카메라의 z위치는 비추려는 대상보다 뒤에있어야 하니까 자체적으로 z값을 준다.
//    }

//}
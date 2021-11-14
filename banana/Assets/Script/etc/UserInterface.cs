using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public Slider HealthBar;

    private void Update()
    {

        if (Input.GetKey(KeyCode.H))
        {
            HealthBar.value -= 0.1f;
        }
    }
}

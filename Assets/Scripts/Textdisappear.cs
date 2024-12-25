using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textdisappear : MonoBehaviour
{
    public Text uiText;
    public float disappearAfterSeconds = 3f;
    private float timeToDisappear;

    void Start()
    {
        timeToDisappear = Time.time + disappearAfterSeconds;
    }

    void Update()
    {
        if (Time.time >= timeToDisappear && uiText != null)
        {
            uiText.enabled = false;










        }
    }
}

   
   
                

    

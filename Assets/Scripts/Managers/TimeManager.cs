using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoSingleton<TimeManager>
{
    [SerializeField] Text countDownText;

    [SerializeField] float minutes = 5;
    [SerializeField] float seconds = 0;
    [SerializeField] float miliseconds = 0;

    public bool IsTimeOut
    {
        get
        {
            return  minutes <= 0f &&
                    seconds <= 0f && 
                    miliseconds <= 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTimeOut) return;

        if (miliseconds <= 0)
        {
            if (seconds <= 0)
            {
                minutes--;
                seconds = 59;
            }
            else if (seconds >= 0)
            {
                seconds--;
            }

            miliseconds = 100;
        }

        miliseconds -= Time.deltaTime * 100;

        countDownText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, (int)miliseconds);
    }
}

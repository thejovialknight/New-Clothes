using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    public float pauseBlinkLength = 0.1f;

    Text text;
    float currentBlinkCooldown = 0f;
    bool isVisible = true;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (isVisible)
        {
            float timer = LevelManager.Instance.timeElapsed;

            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = (timer % 60).ToString("00");

            text.text = string.Format("{0}:{1}", minutes, seconds);
        }
        else
        {
            text.text = "";
        }

        if(!LevelManager.Instance.InGameAndRunning() && LevelManager.Instance.state == LevelState.Game)
        {
            currentBlinkCooldown -= Time.deltaTime;
            if(currentBlinkCooldown <= 0f)
            {
                currentBlinkCooldown = pauseBlinkLength;

                isVisible = !isVisible;
            }
        }
        else
        {
            isVisible = true;
        }
    }
}

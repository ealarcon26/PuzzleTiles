using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public int timeInSeconds;
    public float minute, seconds;
    public Text timeTxt;
    public bool stopTime = false, tutorialDone = false;
    bool isDead = false;
    bool onceTrigger = true;

    void Update()
    {
        if (seconds < 10)
        {
            timeTxt.text = "0"+minute+":0"+seconds;
            if (minute < 10)
            {
                timeTxt.text = "0"+minute+":0"+seconds;
            }
            else
            {
                timeTxt.text = minute+":0"+seconds;
            }
        }
        else
        {
            if (minute < 10)
            {
                timeTxt.text = "0"+minute+":"+seconds;
            }
            else
            {
                timeTxt.text = minute+":"+seconds;
            }
        }

        if (minute <= 0 && seconds <= 0 && !isDead)
        {
            GameObject.Find("PanelFinish").GetComponent<Animator>().SetBool("GameFinish", true);
            GameObject.Find("TitleFinish").GetComponent<Text>().text = "GAME OVER";
            GameObject.Find("ScoreFinish").GetComponent<Text>().text = "Score: "+ GameObject.Find("DataScript").GetComponent<DataScript>().playerScore;
            isDead = true;
        }

        if (tutorialDone && onceTrigger)
        {
            StartCoroutine(startTimer());
            onceTrigger = false;
        }
    }

    public IEnumerator startTimer()
    {
            if (minute > 0) 
            { 
                // minute--; 
                while(minute > 0 && !stopTime)
                {
                    if (seconds <= 0 && minute > 0 && !stopTime)
                    {
                        minute -= 1;
                        seconds = 60;
                    }

                    while(seconds > 0 && !stopTime)
                    {
                        seconds--;
                        yield return new WaitForSeconds(1);
                    }
                }
            }
            else 
            {
                while(seconds > 0 && !stopTime)
                {
                    seconds--;
                    yield return new WaitForSeconds(1);
                }
                
                if (seconds <= 0)
                {
                    // StartCoroutine(gameFinish());
                }
            }
    }
}

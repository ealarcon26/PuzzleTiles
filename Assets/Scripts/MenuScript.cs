using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject pausePanel, pausePanelBlock, settingsPanel, menuPanel;
    public GameObject onOffSoundBtn;

    void Awake()
    {
        pausePanelBlock.SetActive(false);
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void pauseClick()
    {
        togglePauseResume();
        pausePanelBlock.SetActive(true);
        pausePanel.GetComponent<Animator>().SetBool("isPaused", true);
    }
    
    public void resumeClick()
    {
        togglePauseResume();
        pausePanelBlock.SetActive(false);
        pausePanel.GetComponent<Animator>().SetBool("isPaused", false);
    }

    public void settingsClick()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void backSettingsClick()
    {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void quitClick()
    {
        Application.Quit();
    }

    public void toggleSound()
    {
        float volumeToggle = 0.5f;
        if (onOffSoundBtn.transform.GetChild(0).GetComponent<Text>().text == "ON")
        {
            onOffSoundBtn.transform.GetChild(0).GetComponent<Text>().text = "OFF";
            volumeToggle = 0;
        }
        else
        {
            onOffSoundBtn.transform.GetChild(0).GetComponent<Text>().text = "ON";
            volumeToggle = 0.5f;
        }

        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("SoundObject"))
        {
            obj.GetComponent<AudioSource>().volume = volumeToggle;
        }
    }

    public void restartClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }

    public void togglePauseResume()
    {
        var timerScript = GameObject.Find("GameManager").GetComponent<TimerScript>();
        GameObject.Find("GameManager").GetComponent<TimerScript>().stopTime = !timerScript.stopTime;
        StartCoroutine(timerScript.startTimer());
    }
}

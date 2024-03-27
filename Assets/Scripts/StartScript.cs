using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    [SerializeField]
    int countDownNumber = 3;

    public Text countDownText;
    public GameObject countDownAnimation;
    public GameObject startPanel;
    public GameObject tileList;
    public Animator playerAnimator;
    public GameObject dataList;
    public GameObject panelTimer;
    public GameObject panelScore;

    void Awake()
    {
        panelTimer.SetActive(false);
        panelScore.SetActive(false);
        dataList = GameObject.Find("DataScript");
        countDownAnimation.SetActive(false);
        countDownText.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        StartCoroutine(startCountdown());
    }

    IEnumerator startCountdown()
    {
        playerAnimator.SetBool("isGameStart", true);
        startPanel.SetActive(false);
        countDownAnimation.SetActive(true);
        countDownText.gameObject.SetActive(true);
        while(countDownNumber > 0 && countDownAnimation.activeInHierarchy)
        {
            GameObject.Find("CountdownSound").GetComponent<AudioSource>().Play();
            countDownText.text = countDownNumber.ToString();
            countDownNumber--;
            yield return new WaitForSeconds(1);
        }
        countDownText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        countDownAnimation.SetActive(false);
        tileList.SetActive(true);
        dataList.GetComponent<DataScript>().getQuestion();
        startPanel.transform.parent.gameObject.SetActive(false);
        panelTimer.SetActive(true);
        panelScore.SetActive(true);
        StartCoroutine(GetComponent<TimerScript>().startTimer());
    }

    public void gameRestart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("StartScene");
    }
}

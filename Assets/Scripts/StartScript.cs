using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    [SerializeField]
    int countDownNumber = 3;

    public Text countDownText;
    public GameObject countDownAnimation;
    public GameObject startPanel;
    public GameObject tileList;
    public Animator playerAnimator;

    void Awake()
    {
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
            countDownText.text = countDownNumber.ToString();
            countDownNumber--;
            yield return new WaitForSeconds(1);
        }
        countDownText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        countDownAnimation.SetActive(false);
        tileList.SetActive(true);
    }
}

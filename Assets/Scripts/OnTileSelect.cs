using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnTileSelect : MonoBehaviour
{
    [SerializeField]
    Material wrongMat, correctMat, neutralMat;
    public GameObject dataList;
    public GameObject playerCharacter;
    public bool isCorrect = false;

    void Awake()
    {
        playerCharacter = GameObject.FindWithTag("Player");
        dataList = GameObject.Find("DataScript");
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            var isPaused = GameObject.Find("GameManager").GetComponent<TimerScript>().stopTime;
            if (isPaused) {
                if (EventSystem.current.IsPointerOverGameObject()) {
                    return;
                }
            }
            
            GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
            if (gameObject == getClickedObj(out RaycastHit hit))
            {
                checkAnswer();
                playerCharacter.GetComponent<CharacterMovement>().selectedTile = gameObject;
            }
        }
    }

    GameObject getClickedObj(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray  = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }

    public async void checkAnswer()
    {
        if (isCorrect)
        {
            gameObject.GetComponent<MeshRenderer>().material = correctMat;
            dataList.GetComponent<DataScript>().correctCount++;
            if (dataList.GetComponent<DataScript>().correctCount == dataList.GetComponent<DataScript>().selectedQuestion.CorrectAnswers.Length)
            {
                GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
                if (dataList.GetComponent<DataScript>().usedQuestions.Count == 5)
                {
                    var timerScript = GameObject.Find("GameManager").GetComponent<TimerScript>();
                    timerScript.stopTime = true;
                    GameObject.Find("PanelFinish").GetComponent<Animator>().SetBool("GameFinish", true);
                    GameObject.Find("TitleFinish").GetComponent<Text>().text = "GAME COMPLETE";
                    GameObject.Find("ScoreFinish").GetComponent<Text>().text = "Score: "+ dataList.GetComponent<DataScript>().playerScore;
                }
                else
                {
                    StartCoroutine(waitForDelayThenReset());
                    dataList.GetComponent<DataScript>().correctCount = 0;
                    dataList.GetComponent<DataScript>().tileTexts.Clear();
                    dataList.GetComponent<DataScript>().selectedTiles.Clear();
                    dataList.GetComponent<DataScript>().playerScore += 50;
                    await dataList.GetComponent<DataScript>().getQuestion();
                }
                
            }
        }
        else
        {
            if (dataList.GetComponent<DataScript>().playerScore >= 10)
            {
                dataList.GetComponent<DataScript>().playerScore -= 10;
            }
            
            GameObject.Find("WrongSound").GetComponent<AudioSource>().Play();
            gameObject.GetComponent<MeshRenderer>().material = wrongMat;
            dataList.GetComponent<DataScript>().correctCount = 0;
            StartCoroutine(waitForDelayThenReset());
        }
    }

    IEnumerator waitForDelayThenReset()
    {
        yield return new WaitForSeconds(0.5f);
        foreach(TextMeshPro obj in dataList.GetComponent<DataScript>().tileTexts)
        {
            obj.transform.parent.GetComponent<MeshRenderer>().material = neutralMat;
            dataList.GetComponent<DataScript>().correctCount = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TileData {
    public string Title;
    public string[] CorrectAnswers;
    public string[] WrongAnswers;
    public bool isDone;
    public bool isCorrect;
}

public class DataScript : MonoBehaviour
{
    public List<TileData> tileDataList;
    public List<int> usedQuestions;
    public TileData selectedQuestion;
    public List<int> selectedTiles;
    public List<TextMeshPro> tileTexts = new List<TextMeshPro>();
    public TextMeshPro titleText;
    public int correctCount = 0;
    public Text scoreText;
    public int playerScore = 0;
    public bool roundFinish = false;
    public Material neutralMat;

    void Awake()
    {
        tileDataList.Add(new TileData {
            Title = "Root System",
            CorrectAnswers = new string[] {
                "Absorbs water and nutrients",
                "Anchors the plant in the soil",
                "Stores limited energy reserves",
                "Interacts with soil organisms for mutual benefits"
            },
            WrongAnswers = new string[] {
                "Absorbs sunlight for energy",
                "Floats freely in the air",
                "Communicates with extraterrestrial beings",
                "Stores water by releasing it into the atmosphere",
                "Roots are made of metal"
            },
            isDone = false
        });

        tileDataList.Add(new TileData {
            Title = "Vascular System",
            CorrectAnswers = new string[] {
                "Transports water, nutrients, and sugars throughout the plant",
                "Provides support through mechanical tissues",
                "Helps in signaling between plant parts",
                "Absorbs sunlight for energy production"
            },
            WrongAnswers = new string[] {
                "Transports sand instead of water",
                "Sends messages through Morse code",
                "Provides structural support by emitting a strong odor",
                "Helps the plant fly like a helicopter",
                "Vascular tissues are composed of rubber instead of cells"
            },
            isDone = false
        });

        tileDataList.Add(new TileData {
            Title = "Leaves",
            CorrectAnswers = new string[] {
                "Produce food through photosynthesis",
                "Exchange gases for respiration",
                "Regulate water loss through transpiration",
                "Provide shade and protection for the plant"
            },
            WrongAnswers = new string[] {
                "Perform photosynthesis using moonlight",
                "Breathe in carbon dioxide and release nitrogen",
                "Control water loss by secreting excess water into the soil",
                "Leaves are made of glass",
                "Leaves have wings for aerial transportation"
            },
            isDone = false
        });

        tileDataList.Add(new TileData {
            Title = "Stem",
            CorrectAnswers = new string[] {
                "Supports the plant's structure",
                "Stores limited nutrients and water",
                "Transports materials between roots and leaves",
                "Facilitates the growth of new branches and leaves"
            },
            WrongAnswers = new string[] {
                "Supports the plant by deflating like a balloon",
                "Stores food by converting it into rock",
                "Transports materials by teleportation",
                "Grows underground",
                "Stems are composed of jelly instead of plant tissues"
            },
            isDone = false
        });

        tileDataList.Add(new TileData {
            Title = "Shoot System",
            CorrectAnswers = new string[] {
                "Consists of stems, leaves, and branches",
                "Aids in growth and development",
                "Produces flowers for reproduction",
                "Helps in dispersing seeds and fruits"
            },
            WrongAnswers = new string[] {
                "Consists of invisible components",
                "Helps the plant teleport to different locations",
                "Reproduces by cloning itself instantly",
                "Shoots lasers for defense",
                "Shoot system is controlled by artificial intelligence"
            },
            isDone = false
        });
    }
    void Update()
    {
        scoreText.text = playerScore.ToString();
    }

    public async Task setTileText()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("tileText"))
        {
            tileTexts.Add(obj.GetComponent<TextMeshPro>());
        }
    }
    public async Task getQuestion()
    {   
        resetTiles();
        titleText = GameObject.Find("TitleTextMesh").GetComponent<TextMeshPro>();
        int randomNumber;
        if (usedQuestions.Count != 5)
        {
            do {
                randomNumber = Random.Range(0, 5);
            } 
            while (usedQuestions.Contains(randomNumber));

            usedQuestions.Add(randomNumber);
            selectedQuestion = tileDataList[randomNumber];
            titleText.text = selectedQuestion.Title;
        }

        await setTileText();
        await setWrongQuestions();
        await setCorrectQuestions();
    }

    async Task setWrongQuestions()
    {
        int wrongAnswerLength = selectedQuestion.WrongAnswers.Length;
        int increasingNumber = 0;
        while(wrongAnswerLength > 0)
        {
            int randomTile = 0;
            do {
                    randomTile = Random.Range(0, 9);
                } 
            while (selectedTiles.Contains(randomTile));

            selectedTiles.Add(randomTile);
            tileTexts[randomTile].transform.parent.GetComponent<OnTileSelect>().isCorrect = false;
            tileTexts[randomTile].text = selectedQuestion.WrongAnswers[increasingNumber].ToString();
            increasingNumber++;
            wrongAnswerLength--;
        }
        Debug.Log("Done 1");
    }
    async Task setCorrectQuestions()
    {
        int correctAnswerLength = selectedQuestion.CorrectAnswers.Length;
        int increasingNumber = 0;

        while(correctAnswerLength > 0)
        {
            int randomTile = 0;
            do {
                    randomTile = Random.Range(0, 9);
                } 
            while (selectedTiles.Contains(randomTile));

            selectedTiles.Add(randomTile);
            tileTexts[randomTile].transform.parent.GetComponent<OnTileSelect>().isCorrect = true;
            tileTexts[randomTile].text = selectedQuestion.CorrectAnswers[increasingNumber].ToString();
            increasingNumber++;
            correctAnswerLength--;
        }
    }

    void resetTiles()
    {
        
    }
}

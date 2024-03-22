using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("TileGame");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public GameObject selectedTile;

    // Update is called once per frame
    void Update()
    {
        if (selectedTile != null)
        {
            StartCoroutine(startFall());
        }
    }

    IEnumerator startFall()
    {
        transform.GetComponent<Animator>().SetBool("isJump", true);
        yield return new WaitForSeconds(0.1f);
        if (selectedTile != null)
        {
            transform.position = new Vector3(selectedTile.transform.position.x, selectedTile.transform.position.y - 0.5f, selectedTile.transform.position.z);
        }
        
        transform.GetComponent<Animator>().SetBool("isJump", false);
        selectedTile = null;
    }
}

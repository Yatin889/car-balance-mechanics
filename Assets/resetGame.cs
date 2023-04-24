using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetGame : MonoBehaviour
{
   

    private void OnTriggerEnter2D(Collider2D colInfo)
    {
        if(colInfo.CompareTag("map"))
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (colInfo.CompareTag("flag"))
            Debug.Log("YOU HAVE WON THE TERRAIN");
    }
}

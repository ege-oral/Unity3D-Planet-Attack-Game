using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }    
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

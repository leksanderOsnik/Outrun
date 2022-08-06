using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonControls : MonoBehaviour
{
    public void StartGame()
    {
        //start game button reloads the scene and changes game state to 1
        SceneManager.LoadScene("Game");
        GameStateManager.gameState = 1;
        
    }

    public void QuitGame()
    {   //quit button quits the application
        Application.Quit();
    }

    public void ReplayGame()
    {
        //reloads the scene and changes gamestate to 1
        SceneManager.LoadScene("Game");
        GameStateManager.gameState = 1;
       
    }

    public void MainMenu()
    {
        //reloads the scene and sets game state to 0
        SceneManager.LoadScene("Game");
        GameStateManager.gameState = 0;
    }

}

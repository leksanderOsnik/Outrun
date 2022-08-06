using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static int gameState = 0;  //0 = start menu; 1 = In game; 2 = Game Over
    public GameObject gameOverPanel;
    public GameObject startMenuPanel;
    public GameObject inGamePanel;
    void Start()
    {
        
        ChangeGameState();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeGameState();
    }

    public void ChangeGameState() 
    {   
        if (gameState == 0)
        {
            //sets the appropriate UI panel as active
            inGamePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            startMenuPanel.SetActive(true);
            
          
            
        }

        if (gameState == 1)
        {
            //sets the appropriate UI panel as active
            gameOverPanel.SetActive(false);
            startMenuPanel.SetActive(false);
            inGamePanel.SetActive(true);
            
        }

        if (gameState == 2)
        {
            //sets the appropriate UI panel as active
            gameOverPanel.SetActive(true); 
            startMenuPanel.SetActive(false);
            inGamePanel.SetActive(false);

        }

       

    }




}

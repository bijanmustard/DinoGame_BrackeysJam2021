using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Code © Bijan Pourmand
 * Authored 8/22/21
 * Script for GameController, handles win/lose game
 */

public class GameController : MonoBehaviour
{
    public static bool gameStart;
    public static bool gameEnd;
    protected static bool gameWon;
    protected static bool gameLost;


    //WinGame is called to trigger winning the game.
    public static void WinGame()
    {
        if (!gameEnd)
        {
            gameWon = true;
            gameLost = false;
            gameEnd = true;
            OnGameWon();
        }
    }

    //OnGameWon is the sequence of events for winning the game.
    protected static void OnGameWon() {
        Debug.Log("Game won!");
        SceneManager.LoadScene("Credits");

    }

    //GameOver is called when the player has a game over.
    public static void GameOver()
    {
        if (!gameEnd)
        {
            gameLost = true;
            gameWon = false;
            gameEnd = true;
            OnGameOver();
        }
    }
    //OnGameOver is the sequence of events for losing the game.
    protected static void OnGameOver() {
        Debug.Log("Game Over :(");
        SceneManager.LoadScene("Title_Screen");
    }
}

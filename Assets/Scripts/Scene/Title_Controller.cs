using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Code © Bijan Pourmand
 * Authored 8/22/21
 * Script for title screen funcs
 */

public class Title_Controller : MonoBehaviour
{
    //StartGame starts the game
    public void StartGame()
    {
        GameController.StartGame();

    }

    //HowToPlay goes to the how to play screen
    public void HowToPlay()
    {
        Debug.Log("Load How To Play screen here!");
    }

    //Credits pulls up the credits
    public void ToCredits()
    {
        GameController.ToCredits();
    }
}

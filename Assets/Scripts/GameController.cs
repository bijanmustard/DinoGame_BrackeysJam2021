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
    public static GameMode curMode = GameMode.Menu;
    

    //Set Game Mode sets the game mode, toggling cursor accordingly.
    public static void SetGameMode(GameMode mode)
    {
        //1. Set cur mode
        curMode = mode;
        //2. Toggle cursor depending on mode
        if(curMode == GameMode.Game)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //StartGame is called to start the game
    public static void StartGame()
    {
        //1. Load main scene
        SetGameMode(GameMode.Game);
        SceneManager.LoadScene("Level1");
    }

    //ReturnToMenu returns to the main menu.
    public static void ReturnToMenu()
    {
        SceneManager.LoadScene("Title_Screen");
        SetGameMode(GameMode.Menu);
    }

    //ToCredits takes the player to the credits scene.
    public static void ToCredits()
    {
        SceneManager.LoadScene("Credits");
        SetGameMode(GameMode.Menu);
    }
}

public enum GameMode
{
    Menu,
    Game
}

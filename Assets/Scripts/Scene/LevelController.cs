using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

/*
 * Code © Bijan Pourmand
 * Authored 8/23/21
 * Script for level controller
 */

public class LevelController : MonoBehaviour
{
    private static LevelController current;
    public static LevelController Current { get { return current; } }

    public static bool gameStart;
    public static bool gameEnd;
    protected static bool gameWon;
    protected static bool gameLost;

    public int cavemen_destroyed;
    public int cavemen_caged;
    public readonly int caveman_req = 10;

    [SerializeField]
    private AudioClip fanfare;

    private void Awake()
    {
        //1. Set single ref
        if (current != null && current != this) Destroy(gameObject);
        else current = this;
        //2. If game mode somehow isn't game, set to game
        if (GameController.curMode != GameMode.Game) GameController.SetGameMode(GameMode.Game);
    }

    private void Start()
    {
        StartCoroutine(StartIE());
    }

    //StartIE is called upon starting each level to show the intro sequence.
    IEnumerator StartIE()
    {
        //1. Get ref to player and lock movement
        Dino_Move player = FindObjectOfType<Dino_Move>();
        player.canMove = false;
        //2. Play anim and wait
        UI_LevelTitle title = FindObjectOfType<UI_LevelTitle>();
        title.ToggleUI(true);
        yield return new WaitForSeconds(2f);
        //3. Reactivate player and start game
        title.ToggleUI(false);
        yield return new WaitForSeconds(1f);
        player.canMove = true;
    }

    //AddCaveman adds a caveman to the caveman tally. 
    //True = caged, false = destroyed
    public void AddCavemanCt(bool spared)
    {
        // 1. Add to tally
        if (spared) cavemen_caged++;
        else cavemen_destroyed++;
        // 2. If meets req limit, win game
        if((cavemen_caged + cavemen_destroyed) >= caveman_req)
        {
            WinGame();
        }

    }

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
    public static void OnGameWon()
    {
        Debug.Log("Game won!");
        current.StartCoroutine(LevelController.Current.GameWinIE());

    }

   public IEnumerator GameWinIE()
    {
        //1. Lock player movement
        Dino_Move player = FindObjectOfType<Dino_Move>();
        player.canMove = false;
        //2. Show victory message
        UI_LevelTitle blurb = FindObjectOfType<UI_LevelTitle>();
        blurb.WinUI();
        //Play victory jingle
        AudioSource aSource = Camera.main.GetComponent<AudioSource>();
        //AudioClip clip = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Audio/SFX/freesoundslibrary/Tada-sound.mp3", typeof(AudioClip));
        aSource.clip = fanfare;
        aSource.PlayDelayed(0.8f);
        yield return new WaitForSeconds(2.5f);
        //3. To Credits
        GameController.ToCredits();
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
    protected static void OnGameOver()
    {
        Debug.Log("Game Over :(");
        GameController.ReturnToMenu(); ;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Code © Bijan Pourmand
 * Authored 8/28/21
 * Script for level title card blurb
 */

public class UI_LevelTitle : MonoBehaviour
{
    protected Text myText;
    protected Animator anim;
    protected string[] victoryLines = { "vengeance royale!", "cavemen cooked!" };

    private void Awake()
    {
        myText = GetComponent<Text>();
        anim = GetComponent<Animator>();
    }

    //ToggleUI enters/exits the title card.
    public void ToggleUI(bool tog)
    {
        if (tog)
        {
            myText.text = string.Format("mission 1\ndestroy {0} cavemen!", LevelController.Current.caveman_req);
            anim.SetTrigger("Enter");
        }
        else anim.SetTrigger("Exit");
    }

    //WinUI shows the level complete UI.
    public void WinUI()
    {
        myText.text = victoryLines[0];
        anim.SetTrigger("Enter");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Code © Bijan Pourmand
 * Authored 8/28/21
 * Script for CMVCounter UI
 */

public class GameUI_CVMCounter : MonoBehaviour
{
    Text myText;

    private void Awake()
    {
        myText = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        myText.text = string.Format("{0}/{1}", LevelController.Current.cavemen_caged + LevelController.Current.cavemen_destroyed,
            LevelController.Current.caveman_req);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Code © Bijan Pourmand
 * Authored 8/25/21
 * Script for DBGUI
 */

public class DBGUI: MonoBehaviour
{
    protected Text myText;

    private void Awake()
    {
        myText = GetComponent<Text>();
    }
}

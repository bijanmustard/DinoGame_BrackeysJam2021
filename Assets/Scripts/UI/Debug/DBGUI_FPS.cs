using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Code © Bijan Pourmand
 * Authored 8/25/21
 * Script for DBGUI fps counter
 */

public class DBGUI_FPS : DBGUI
{

    private void Update()
    {
        myText.text = (1f / Time.deltaTime).ToString(); ;
    }
}

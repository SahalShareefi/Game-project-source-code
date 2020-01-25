using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Buttons : GroundNode
{
    public bool selected = false;
    public bool built = false;


    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 20, Screen.height / 15, 100, 30), "main Building"))
        {
            selected = true;
            built = true;
            //  ChangingLayerInEditMode();
        }

    }
}

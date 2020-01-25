using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_EditGameObject : MonoBehaviour
{

    Vector2 place;

    public GameObject Put_Here_The_Building; //rename this to the name of the chosenBuilding later..
    



    //winows (close and open)

    bool are_you_sure_window = false; //appear when Destroy clicked
    bool main_menu_window = false; //the menu which contains all of the edits for the chosenBuilding..(it's basically the background)
    bool building_editing_window_Destrouy = false;



    // bottons in the GUI

    bool Gameobject_clicked = false;
    bool Destroy_botton_cliked = false;
    bool yes_botton = false;
    bool no_botton = true;

   


    private void OnMouseDown ()
    {
        //if(/*Input.GetMouseButtonDown(0) ||*/ gameObject.tag=="Building")
        Gameobject_clicked = true;


    }






    private void OnGUI()
    {
        //main menu (layer 1)
        if (main_menu_window)
        {
            place = new Vector2((Screen.width - 300) / 2, Screen.height - 150);
            //the back ground

            GUI.Box(new Rect(place.x, place.y, 300, 150), "Choose Action!");

            //destroy botton
            if (GUI.Button(new Rect(place.x + 20, place.y + 60, 70, 40), "Destroy"))
            {

                building_editing_window_Destrouy = true;

            }

            if (GUI.Button(new Rect(place.x + 100, place.y + 60, 70, 40), "Rotate"))
            {

                transform.Rotate(0, 90, 0);

            }

            if (GUI.Button(new Rect(place.x+260, place.y, 40, 40), "X"))
            {
                Gameobject_clicked = false;
                main_menu_window = false;
            }
        }



        if (building_editing_window_Destrouy)
        {

            place = new Vector2((Screen.width - 280) / 2, (Screen.height - 100)/2);
            GUI.Box(new Rect(place.x, place.y + 20, 280, 100), "are you sure you want to destroy this chosenBuilding?");

            if (GUI.Button(new Rect(place.x + 50, place.y + 60, 50, 40), "yes"))
                Destroy(Put_Here_The_Building);

            else if (GUI.Button(new Rect(place.x + 150, place.y + 60, 50, 40), "no"))
                building_editing_window_Destrouy = false;
        }
        

        // when click on the chosenBuilding..

        if (Gameobject_clicked)
        {
            main_menu_window = true;
           
        }
        

    }//end of method



    void Start()
    {
    }

    void Update()
    {

        

    }
}










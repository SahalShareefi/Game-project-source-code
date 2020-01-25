using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : GroundNode {
    /*
    public bool selected = false;
    public bool readyToBuild = false;
    BuildManager gameManagerInstance;
    public static shop shop_instance;

    private void Awake()
    {
		if (shop_instance == null)
            shop_instance = this;
		else
			DestroyObject(this);
    }

    private void Start()
    {
        gameManagerInstance = BuildManager.instance;
    }
    public void chooseStandardBuilding()
    {
        //print(selected + "+" + readyToBuild);
        if(readyToBuild == true && building_Pref_Holder.tag == "BeforeBuilding")
           Destroy(building_Pref_Holder);

        BuildManager.instance.SetCurrentBuilding(gameManagerInstance.building_Types_Array);
		print(gameManagerInstance.GetCurrentBuilding().name);
        selected = true;
        readyToBuild = true;  
    }
    public void chooseBuilding2()
    {
       // print(selected + "+" + readyToBuild);
        if (readyToBuild == true && building_Pref_Holder.tag == "BeforeBuilding")
            Destroy(building_Pref_Holder);

        Debug.Log(gameManagerInstance.BuildingPref2.name);
		BuildManager.instance.SetCurrentBuilding(gameManagerInstance.BuildingPref2);
        selected = true;
        readyToBuild = true;
    }
     void Update()// once player press in empty
    {
        
        if (Input.GetMouseButtonDown(0))
        {
                if (readyToBuild == true && building_Pref_Holder.tag == "BeforeBuilding" && correct_place == true) // correct_place==true:to not destroy it when it is on red "incorrect place"
                {
                    Destroy(building_Pref_Holder);
                    selected = false;
                    readyToBuild = false;
                }
        } 
    }
    */
}


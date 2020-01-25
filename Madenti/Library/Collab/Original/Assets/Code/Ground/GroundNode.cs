using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroundNode : MonoBehaviour{

    public static GameObject building_Pref_Holder; // ststic to  that will be stored in the DB
    public bool correct_place =true;
    public Vector3 b_Offset { get { return new Vector3(0,2.5f,0); } }


    void Update()
    {
        // do ghosting do and do building
    }

    void OnMouseOver()    //    if u use OnMousEnter() once u click button building_Pref_Holder does not instantiate building_Pref_Holder untel u/(mouse) enter node from out side                                                              
    {
        Do_Ghosting();
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == true)
                return;
                    if (correct_place == true && General_UI.GeneralUI_instance.readyToBuild)
            {

                    Do_Building(building_Pref_Holder);
        }
        else
            print("can not build");
    }
    }

    public void Do_Ghosting()
    {

        if (General_UI.GeneralUI_instance.selected)
        {
            building_Pref_Holder = (GameObject)Instantiate(city_Build_Manager.instance.chosenBuilding.BuildingModel, transform.position + b_Offset, transform.rotation);
            General_UI.GeneralUI_instance.selected = false;
        }
        else if (General_UI.GeneralUI_instance.readyToBuild)
        {
            building_Pref_Holder.GetComponent<Detector_And_Progress>().GhostingColoring();
            building_Pref_Holder.GetComponent<Building_Logic>().Target = this.transform.position + b_Offset;

            // to go back normal build movement 
            // building_Pref_Holder.transform.position = transform.position + b_Offset;
        }
    }

    public void Do_Building(GameObject currentBuilding)
    {
        if (city_Build_Manager.instance.chosenBuilding.Building_Status == Building_Status.NotUsed)
        { 
            StartCoroutine(coloring_building(currentBuilding));
            General_UI.GeneralUI_instance.readyToBuild = false;
            correct_place = false; // to avoid stack building_Pref_Holder

            currentBuilding.GetComponent<Building_Logic>().Choosed_BuildingInfo.input.amount -= 200; // subtract the money 
            currentBuilding.GetComponent<Building_Logic>().Choosed_BuildingInfo.Position = this.transform.position;

            currentBuilding = GameObject.FindGameObjectWithTag("BeforeBuilding");
            if (currentBuilding.transform.childCount > 0)
            {
                foreach (Transform parts in CollectBuildingParts.buildingPartsInstance.buildingParts)
                { parts.gameObject.layer = 0; }
                foreach (Transform parts in CollectBuildingParts.buildingPartsInstance.buildingParts)
                { parts.gameObject.tag = "Building"; }
            }
            else
            {
                currentBuilding.layer = 0;//defailt (raycast can hit)
                currentBuilding.tag = "Building";
            }

            city_Build_Manager.instance.Buildings.Add(new BuildingData(currentBuilding.GetComponent<Building_Logic>().Node,
                currentBuilding.GetComponent<Building_Logic>().Choosed_BuildingInfo.name));
             //   currentBuilding.GetComponent<Building_Logic>().Choosed_BuildingInfo.BuildingModel.transform.position));

          //   city_Build_Manager.instance.BuildingsTemp.Add(currentBuilding.GetComponent<Building_Logic>().Choosed_BuildingInfo.BuildingModel);
            //    city_Build_Manager.instance.BuildingsLogic.Add(currentBuilding.GetComponent<Building_Logic>().Node);
        }
        else if (city_Build_Manager.instance.chosenBuilding.Building_Status == Building_Status.Functional)
        {

        }
    }

    
    /* 
    public void Red_Building_coloring()
    {
        if (correct_place == false)
            GetComponent<Detector_And_Progress>().InCorrectPlaceColoring();
        else
            GetComponent<Detector_And_Progress>().CorrectPlaceColoring();
    }*/

    private IEnumerator coloring_building(GameObject b)
    {
            b.GetComponent<Detector_And_Progress>().coloring_Build_progress();
        b.GetComponent<Building_Logic>().Choosed_BuildingInfo.StartConstructing(this,b);// this node position
             yield return new WaitForSecondsRealtime((float)b.GetComponent<Building_Logic>().Choosed_BuildingInfo.timeNeededToCunstruct().TotalSeconds);
        b.GetComponent<Detector_And_Progress>().Finish_Build_progress();
    }

}
